/*-- BEGIN Menu Type --*/

insert into GNRL_LOOKUPGROUP (LOOKUPGROUPID,NAME) select 108,'Menu Type'
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108001,'','Standard','Standard Menu',1,1,108
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108002,'','Vegeterian','Vegeterian Menu',2,1,108
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108003,'','Special','Special Menu',3,1,108
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108004,'','Singular','Singular Menu',4,1,108
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108005,'','Lunch Option 1','Lunch Option 1 Menu',4,1,108
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108006,'','Lunch Option 2','Lunch Option 2 Menu',4,1,108
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108007,'','Snack','Snack Menu',4,1,108
insert into GNRL_LOOKUPITEM (LOOKUPITEMID,FIELDVALUE,FIELDTEXT,DESCRIPTION,ITEMORDER,ISACTIVE,LOOKUPGROUPID) select 108008,'','Sack Lunch','Sack Lunch Menu',4,1,108

/*-- END Menu Type --*/