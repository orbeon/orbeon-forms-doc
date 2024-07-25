# Docker

[Orbeon Forms PE only]

## Docker images

Multiple Docker images are available from the [Orbeon repository on Docker Hub](https://hub.docker.com/u/orbeon):

- [`orbeon/orbeon-forms`](https://hub.docker.com/r/orbeon/orbeon-forms): the Orbeon Forms application running on Tomcat
- [`orbeon/postgres`](https://hub.docker.com/r/orbeon/postgres): a PostgreSQL database prepopulated with the Orbeon Forms database schema

The `orbeon/orbeon-forms` image can be run as a standalone container, as it contains an SQLite database with demo forms. This is intended for evaluation purposes only, and you will probably want to use another database in production. For this, you can use the `orbeon/postgres` image, which contains a PostgreSQL database prepopulated with the Orbeon Forms database schema. See the [Docker Compose configuration](#docker-compose-configuration) below for an example of how to run Orbeon Forms with PostgreSQL.

## Evaluation mode

To create a container from the `orbeon/orbeon-forms` image, with the embedded SQLite database, run the following `docker` command:

```bash
docker create \
    --name orbeon-forms-with-sqlite \
    -p 8080:8080 \
    -v ~/.orbeon/license.xml:/usr/local/tomcat/webapps/orbeon/WEB-INF/resources/config/license.xml \
    orbeon/orbeon-forms:2023.1.3-pe
```

Make sure to replace `~/.orbeon/license.xml` with the path to your license file and to use another port if `8080` is already in use on your machine.

To start the container:

```bash
docker start -a orbeon-forms-with-sqlite
```

You can then access Orbeon Forms using the following URL: [http://localhost:8080/orbeon](http://localhost:8080/orbeon).

While using SQLite, keep in mind that all modifications to the form definitions and data will be stored in the container's filesystem and will be lost if the container is deleted. The file storing the SQLite database is located at `/usr/local/tomcat/webapps/orbeon/WEB-INF/orbeon-demo.sqlite` inside the container.

### Storing SQLite data outside the container

To persist the data outside the container, you can copy the `orbeon-demo.sqlite` file to the host and mount it inside the container while creating the container from the image, by adding the following argument:

```bash
-v /path/to/orbeon-demo.sqlite:/usr/local/tomcat/webapps/orbeon/WEB-INF/orbeon-demo.sqlite
```

This is not recommended for production use.

This involves creating a first container just to extract the `orbeon-demo.sqlite` file, then creating a second container with the mounted file. Alternatively, you can also extract the `orbeon-demo.sqlite` file from the [Orbeon Forms WAR file](https://www.orbeon.com/download).

## Docker Compose configuration

The following `docker-compose.yml` file can be used to start Orbeon Forms with a PostgreSQL database:

```yaml
version: '3.8'
services:
  orbeon-forms:
    image: orbeon/orbeon-forms:2023.1.3-pe
    ports:
      - ${ORBEON_TOMCAT_PORT:-8080}:8080
    volumes:
      - ${ORBEON_PROPERTIES_FILE:-./properties-local.xml}:/usr/local/tomcat/webapps/orbeon/WEB-INF/resources/config/properties-local.xml
      - ${ORBEON_TOMCAT_CONTEXT_FILE:-./orbeon.xml}:/usr/local/tomcat/conf/Catalina/localhost/orbeon.xml
#      - ${ORBEON_LOG4J2_FILE:-./log4j2.xml}:/usr/local/tomcat/webapps/orbeon/WEB-INF/resources/config/log4j2.xml
    secrets:
      - source: license
        target: /usr/local/tomcat/webapps/orbeon/WEB-INF/resources/config/license.xml
    depends_on:
      - postgres
    networks:
      - orbeon-forms-and-postgres
  postgres:
    image: orbeon/postgres:2023.1.3-pe
    restart: always
    ports:
      - ${ORBEON_POSTGRES_PORT:-5432}:5432
    volumes:
      - pgdata:/var/lib/postgresql/data
    environment:
      POSTGRES_DB: ${ORBEON_POSTGRES_DB:-orbeon}
      POSTGRES_USER: ${ORBEON_POSTGRES_USER:-orbeon}
      POSTGRES_PASSWORD: ${ORBEON_POSTGRES_PASSWORD:-orbeon}
#      POSTGRES_PASSWORD_FILE: /run/secrets/postgres_password
#    secrets:
#      - postgres_password
    networks:
      - orbeon-forms-and-postgres
networks:
  orbeon-forms-and-postgres:
    driver: bridge
volumes:
  pgdata:
    name: ${ORBEON_POSTGRES_VOLUME:-orbeon_pgdata}
secrets:
  license:
    file: ${ORBEON_LICENSE_FILE:-~/.orbeon/license.xml}
#  postgres_password:
#    file: ${ORBEON_POSTGRES_PASSWORD_FILE:-postgres_password.txt}
```

This Docker Compose file is meant as an example and can be customized to fit your needs.

Outside the `docker-compose.yml` file, you will also need at least three other files:

- the license file (like in the single container case)
- an Orbeon Forms properties file to specify that PostgreSQL must be used instead of SQLite
- a Tomcat context configuration file to specify the PostgreSQL data source

### Properties file

The `properties-local.xml` properties file needs to contain at least the following:

```xml
<properties xmlns:xs="http://www.w3.org/2001/XMLSchema"
            xmlns:oxf="http://www.orbeon.com/oxf/processors">
    <property as="xs:string"  name="oxf.crypto.password"               value="CHANGE THIS PASSWORD"/>
    <property as="xs:string"  name="oxf.fr.persistence.provider.*.*.*" value="postgresql"/>
    <property as="xs:boolean" name="oxf.fr.persistence.sqlite.active"  value="false"/>
</properties>
```

Choose a strong password for the `oxf.crypto.password` property. More information about this property and other properties in general can be found in the [Properties section](https://doc.orbeon.com/configuration/properties) of the documentation.

### Tomcat context configuration

The `orbeon.xml` Tomcat context configuration file needs to contain at least the following:

```xml
<Context path="/orbeon">
    <Resource
        name="jdbc/postgresql"
        driverClassName="org.postgresql.Driver"
    
        auth="Container"
        type="javax.sql.DataSource"
    
        initialSize="3"
        maxActive="10"
        maxIdle="10"
        maxWait="30000"
    
        poolPreparedStatements="true"
    
        testOnBorrow="true"
        validationQuery="select 1"
    
        username="orbeon"
        password="orbeon"
        url="jdbc:postgresql://postgres:5432/orbeon?useUnicode=true&amp;characterEncoding=UTF8&amp;socketTimeout=30&amp;tcpKeepAlive=true"/>
</Context>
```

If you've changed the default PostgreSQL service name, database name, user, password, or port in the Docker Compose configuration, make sure to update the `url`, `username`, and `password` attributes accordingly.

In particular, note that the `postgres` hostname in the `url` attribute refers to the name of the PostgreSQL service in the Docker Compose configuration.

### Environment variables

Using the example Docker Compose configuration above, you can customize the behavior of the containers by editing the configuration file directly or by using the following environment variables:

| Environment variable         | Default value            | Description                                     |
|------------------------------|--------------------------|-------------------------------------------------|
| `ORBEON_LICENSE_FILE`        | `~/.orbeon/license.xml`  | Path to the Orbeon Forms license file           |
| `ORBEON_PROPERTIES_FILE`     | `./properties-local.xml` | Path to the Orbeon Forms properties file        |
| `ORBEON_TOMCAT_CONTEXT_FILE` | `./orbeon.xml`           | Path to the Tomcat context configuration file   |
| `ORBEON_TOMCAT_PORT`         | 8080                     | Tomcat port                                     |
| `ORBEON_POSTGRES_PORT`       | 5432                     | PostgreSQL port                                 |
| `ORBEON_POSTGRES_DB`         | orbeon                   | PostgreSQL database                             |
| `ORBEON_POSTGRES_USER`       | orbeon                   | PostgreSQL user                                 |
| `ORBEON_POSTGRES_PASSWORD`   | orbeon                   | PostgreSQL password                             |
| `ORBEON_POSTGRES_VOLUME`     | orbeon_pgdata            | Docker volume used to store the PostgreSQL data |

The values of those environment variables can be set in a `.env` file in the same directory as the `docker-compose.yml` file.

Note that the PostgreSQL password can be specified via a Docker secret file. To do so, uncomment the lines related to `postgres_password` in the Docker Compose configuration file.

### Running the containers using Docker Compose

To start Orbeon Forms with PostgreSQL, run the following command in the directory containing the `docker-compose.yml` file:

```bash
docker compose up
```

You can also specify the location of the Docker Compose file using the `-f` option.

## JDBC drivers and other databases

The `orbeon/orbeon-forms` image contains the JDBC drivers for SQLite and PostgreSQL. Other JDBC drivers are not included for licensing reasons. If you need to use another database, you will need to add the JDBC driver to the image.

This can be done by using the following Dockerfile, using MySQL as an example:

```dockerfile
FROM orbeon/orbeon-forms:2023.1.3-pe

RUN mkdir -p /tmp/orbeon
WORKDIR /tmp/orbeon

# JDBC driver for MySQL
RUN wget https://dev.mysql.com/get/Downloads/Connector-J/mysql-connector-j-9.0.0.tar.gz \
    && tar xvfz mysql-connector-j-9.0.0.tar.gz \
    && mv mysql-connector-j-9.0.0/mysql-connector-j-9.0.0.jar /usr/local/tomcat/lib/ 

# Remove SQLite database and JDBC driver (optional)
RUN rm -f /usr/local/tomcat/webapps/orbeon/WEB-INF/orbeon-demo.sqlite \
    && rm -f /usr/local/tomcat/webapps/orbeon/WEB-INF/lib/sqlite-jdbc-*.jar 

# Remove PostgreSQL JDBC driver (optional)
RUN rm -f /usr/local/tomcat/lib/postgresql-*.jar

# Cleanup
RUN rm -rf /tmp/orbeon
```

The customized image above can then be built using the following command:

```bash
docker build -f Dockerfile.mysql -t "orbeon/orbeon-forms-mysql:2023.1.3-pe" .
```

For further information about configuring Orbeon Forms to use a different database, see [Using a relational database](/form-runner/persistence/relational-db.md).

## Logging

By default, Orbeon Forms outputs its logs to the console. To change the logging configuration, you can mount a `log4j2.xml` file inside the container:

```bash
-v /path/to/log4j2.xml:/usr/local/tomcat/webapps/orbeon/WEB-INF/resources/config/log4j2.xml
```

See [Logging](/installation/logging.md) for more information.