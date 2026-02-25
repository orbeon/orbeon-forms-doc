# DB-to-S3 attachment migration

## Availability

[SINCE Orbeon Forms 2025.1.1]

## Introduction

When switching your attachment storage from a relational database to S3 (see [Storing attachments in the filesystem or on S3](/configuration/properties/persistence.md#storing-attachments-in-the-filesystem-or-on-s3)), existing attachments remain in the database. The DB-to-S3 attachment migration tool moves those existing attachments from the database to S3, so that all attachments are stored in a single location.

The tool:

- reads attachments from the `orbeon_form_data_attach` and `orbeon_form_definition_attach` database tables
- uploads them to the configured S3 bucket
- verifies data integrity (size and SHA-256 hash)
- nullifies the `file_content` column in the database after a successful upload

If an attachment already exists on S3 with the correct content, the tool skips the upload and only nullifies the database column.

## Prerequisites

It is highly recommended to back up your database before running this tool. While the tool includes integrity checks, a backup ensures you can recover if anything goes wrong.

## Running with Docker

The tool is available as a Docker image: `orbeon/db-to-s3-attachment-migration`.

### Basic usage

```bash
docker run --rm \
  orbeon/db-to-s3-attachment-migration:2025.1.1-pe \
  --db-url "jdbc:postgresql://host:5432/orbeon" \
  --db-user "orbeon" \
  --db-password "secret" \
  --s3-bucket "my-bucket" \
  --s3-access-key "AKIAIOSFODNN7EXAMPLE" \
  --s3-secret-access-key "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY" \
  --s3-region "us-east-1"
```

### Using environment variables

Instead of command-line arguments, you can use environment variables. To avoid exposing secrets in the command line, you can store them in a file and use Docker's `--env-file` option:

```properties
ORBEON_DB_URL=jdbc:postgresql://host:5432/orbeon
ORBEON_DB_USER=orbeon
ORBEON_DB_PASSWORD=secret
ORBEON_S3_BUCKET=my-bucket
ORBEON_S3_ACCESS_KEY=AKIAIOSFODNN7EXAMPLE
ORBEON_S3_SECRET_ACCESS_KEY=wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
ORBEON_S3_REGION=us-east-1
```

```bash
docker run --rm \
  --env-file migration.env \
  orbeon/db-to-s3-attachment-migration:2025.1.1-pe
```

### JDBC drivers

The Docker image includes the PostgreSQL JDBC driver. For licensing reasons, other database drivers (MySQL, Oracle, SQL Server, DB2) are not included. You must provide the appropriate JDBC driver JAR by mounting it into the `/opt/drivers/` directory:

```bash
docker run --rm \
  -v /path/to/mysql-connector-j-9.2.0.jar:/opt/drivers/mysql-connector-j-9.2.0.jar \
  orbeon/db-to-s3-attachment-migration:2025.1.1-pe \
  --db-url "jdbc:mysql://host:3306/orbeon" \
  --db-user "orbeon" \
  --db-password "secret" \
  --s3-bucket "my-bucket" \
  --s3-access-key "..." \
  --s3-secret-access-key "..."
```

### Network access

The Docker container must be able to reach both the database and the S3 endpoint. If the database runs on the host machine, you may need to use `--network host` or the appropriate Docker networking option for your setup.

## Parameters

### Required

| Parameter                | Environment variable           | Description                 |
|--------------------------|--------------------------------|-----------------------------|
| `--db-url`               | `ORBEON_DB_URL`                | JDBC URL for the database   |
| `--s3-bucket`            | `ORBEON_S3_BUCKET`             | S3 bucket name              |
| `--s3-access-key`        | `ORBEON_S3_ACCESS_KEY`         | AWS access key ID           |
| `--s3-secret-access-key` | `ORBEON_S3_SECRET_ACCESS_KEY`  | AWS secret access key       |

### Optional

| Parameter                | Environment variable              | Default             | Description                                            |
|--------------------------|-----------------------------------|---------------------|--------------------------------------------------------|
| `--db-user`              | `ORBEON_DB_USER`                  | (empty)             | Database username                                      |
| `--db-password`          | `ORBEON_DB_PASSWORD`              | (empty)             | Database password                                      |
| `--db-init-sql`          | `ORBEON_DB_INIT_SQL`              | (empty)             | SQL to execute after connecting                        |
| `--s3-endpoint`          | `ORBEON_S3_ENDPOINT`              | `s3.amazonaws.com`  | S3 endpoint URL                                        |
| `--s3-region`            | `ORBEON_S3_REGION`                | `aws-global`        | S3 region                                              |
| `--s3-base-path`         | `ORBEON_S3_BASE_PATH`             | (empty)             | S3 key prefix                                          |
| `--s3-force-path-style`  | `ORBEON_S3_FORCE_PATH_STYLE`      | `false`             | Use path-style S3 URLs                                 |
| `--parallelism`          | `ORBEON_PARALLELISM`              | `1`                 | Number of rows to process concurrently                 |
| `--dry-run`              | `ORBEON_DRY_RUN`                  | `false`             | Preview what would be migrated, without making changes |

Command-line arguments take precedence over environment variables.

## Dry run

It is recommended to start with a dry run to preview what the tool would do:

```bash
docker run --rm \
  orbeon/db-to-s3-attachment-migration:2025.1.1-pe \
  --db-url "jdbc:postgresql://host:5432/orbeon" \
  --db-user "orbeon" \
  --db-password "secret" \
  --s3-bucket "my-bucket" \
  --s3-access-key "..." \
  --s3-secret-access-key "..." \
  --dry-run
```

In dry-run mode, no data is uploaded to S3 and no database rows are modified.

## Data integrity

The tool verifies each upload by comparing the file size and SHA-256 hash between the database and S3. If a mismatch is detected, the tool aborts the migration and the database content is preserved.

## S3-compatible services

The tool works with any S3-compatible service (e.g. [MinIO](https://min.io/), [Backblaze B2](https://www.backblaze.com/)). Use the `--s3-endpoint` parameter to point to your service. Some S3-compatible services also require `--s3-force-path-style` to be set to `true`.

## See also

- [S3 storage](/form-runner/feature/s3.md)
- [Storing attachments in the filesystem or on S3](/configuration/properties/persistence.md#storing-attachments-in-the-filesystem-or-on-s3)
