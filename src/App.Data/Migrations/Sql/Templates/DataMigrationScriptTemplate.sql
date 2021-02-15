do $do$ 
declare migrationName text := 'migrationName';

begin if not exists (select 1 from "__EFMigrationsHistory" where "MigrationId" = migrationName) then 

    -- put your script there 

    insert into "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    values (migrationName, '3.1.1');
   
end if;
end $do$