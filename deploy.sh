
aws ecr get-login-password --region us-east-1 | docker login --username AWS --password-stdin 234994283525.dkr.ecr.us-east-1.amazonaws.com
docker build -t sim-backend-rep .
docker tag sim-backend-rep:latest 234994283525.dkr.ecr.us-east-1.amazonaws.com/sim-backend-rep:latest
docker push 234994283525.dkr.ecr.us-east-1.amazonaws.com/sim-backend-rep:latest




DOcker Instance
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Sample123*" -e "MSSQL_PID=Express"  -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest 



Backup 
exec msdb.dbo.rds_backup_database 
@source_db_name='ob-logistics', @s3_arn_to_backup_to='arn:aws:s3:::cr-sim-db-backups/2022-april-16.bak', 
@overwrite_S3_backup_file=1;



restore
RESTORE DATABASE oblogistics FROM DISK = N'/var/opt/mssql/backup/2022-april-16.bak' WITH RECOVERY, MOVE "ob-logistics" TO N'/var/opt/mssql/data/2022-april-16.mdf', MOVE 'ob-logistics_log' TO N'/var/opt/mssql/data/2022-april-16_log.ldf'


Data Source=localhost;Initial Catalog=oblogistics;Persist Security Info=False;User ID=sa;Password=Sample123*;
