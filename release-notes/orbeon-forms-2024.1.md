# Orbeon Forms 2024.1

## Upgrading

- Typo in index names - We previously had a typo in the names of some indexes for SQL Server, PostgreSQL, Oracle, and MySQL: some indices began with `orbeon_from` instead of `orbeon_form`. Although this typo doesn't cause any issues, you may want to check for it during your upgrade and rename the affected indices for consistency. We corrected this typo in the DDL we provided in November 2024, so if you created your indices before that date, you likely have this typo.