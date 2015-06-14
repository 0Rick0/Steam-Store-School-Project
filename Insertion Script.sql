DELETE FROM user_achievement;
DELETE FROM achievement;
DELETE FROM user_comment;
DELETE FROM chatmessage;
DELETE FROM friend;
DELETE FROM bought;
DELETE FROM steam_user;
DELETE FROM updateable;
DELETE FROM installable;
DELETE FROM appfile;
DELETE FROM appstoreitem;
DELETE FROM app_pack;
DELETE FROM pack;
DELETE FROM pictures;
DELETE FROM app;
DELETE FROM categorie;

--Categorie
INSERT INTO categorie (categorieId, categorieName, superCategorie) VALUES (1,'All',null);
INSERT INTO categorie (categorieId, categorieName, superCategorie) VALUES (2,'Racing',1);
INSERT INTO categorie (categorieId, categorieName, superCategorie) VALUES (3,'FPS',1);
INSERT INTO categorie (categorieId, categorieName, superCategorie) VALUES (4,'Survival',1);
INSERT INTO categorie (categorieId, categorieName, superCategorie) VALUES (5,'Formula 1',2);
INSERT INTO categorie (categorieId, categorieName, superCategorie) VALUES (6,'City',2);

--App
INSERT INTO app (appId, categorieId, appName) VALUES (1,2,'Dirt3');
INSERT INTO app (appId, categorieId, appName) VALUES (2,6,'Need For Speed Most Wanted');
INSERT INTO app (appId, categorieId, appName) VALUES (3,3,'Battlefield 4');
INSERT INTO app (appId, categorieId, appName) VALUES (4,4,'Terarya');
INSERT INTO app (appId, categorieId, appName) VALUES (5,5,'Formula 1');
INSERT INTO app (appId, categorieId, appName) VALUES (6,3,'Call of Duty Modern Warfare 3');

--pictures
INSERT INTO pictures (appId, picId) VALUES (1,1);
INSERT INTO pictures (appId, picId) VALUES (1,2);
INSERT INTO pictures (appId, picId) VALUES (1,3);
INSERT INTO pictures (appId, picId) VALUES (2,4);
INSERT INTO pictures (appId, picId) VALUES (2,5);
INSERT INTO pictures (appId, picId) VALUES (2,6);
INSERT INTO pictures (appId, picId) VALUES (3,7);
INSERT INTO pictures (appId, picId) VALUES (3,8);
INSERT INTO pictures (appId, picId) VALUES (3,9);
INSERT INTO pictures (appId, picId) VALUES (4,10);
INSERT INTO pictures (appId, picId) VALUES (4,11);
INSERT INTO pictures (appId, picId) VALUES (4,12);
INSERT INTO pictures (appId, picId) VALUES (5,13);
INSERT INTO pictures (appId, picId) VALUES (5,14);
INSERT INTO pictures (appId, picId) VALUES (5,15);
INSERT INTO pictures (appId, picId) VALUES (6,16);
INSERT INTO pictures (appId, picId) VALUES (6,17);
INSERT INTO pictures (appId, picId) VALUES (6,18);

--pack
INSERT INTO pack (packId, packName, description, price, discount) VALUES (1,'Dirt3','Drive around the world!',13.00, 0.000);
INSERT INTO pack (packId, packName, description, price, discount) VALUES (2,'All Racing!','Drive around the world!',26.00, 0.250);--€20~
INSERT INTO pack (packId, packName, description, price, discount) VALUES (3,'Need for Speed Most Wanted','Drive around the world!',13.00, 0.000);
INSERT INTO pack (packId, packName, description, price, discount) VALUES (4,'Battlefield 4','BAM',13.00, 0.000);
INSERT INTO pack (packId, packName, description, price, discount) VALUES (5,'Terarya','yay!',13.00, 0.000);
INSERT INTO pack (packId, packName, description, price, discount) VALUES (6,'Formula 1','Vroem Vroem!',13.00, 0.000);
INSERT INTO pack (packId, packName, description, price, discount) VALUES (7,'Call of Dyty Modern Warfare 3','Piew piew!',13.00, 0.000);

--app_pack
INSERT INTO app_pack (appId,packId) VALUES (1,1);
INSERT INTO app_pack (appId,packId) VALUES (1,2);
INSERT INTO app_pack (appId,packId) VALUES (2,2);
INSERT INTO app_pack (appId,packId) VALUES (5,2);
INSERT INTO app_pack (appId,packId) VALUES (2,3);
INSERT INTO app_pack (appId,packId) VALUES (3,4);
INSERT INTO app_pack (appId,packId) VALUES (4,5);
INSERT INTO app_pack (appId,packId) VALUES (5,6);
INSERT INTO app_pack (appId,packId) VALUES (6,7);

--appstoreitem
INSERT INTO appstoreitem (appId, pos, title, text) VALUES (1,1,'Vroem Vroem','Njewm');
INSERT INTO appstoreitem (appId, pos, title, text) VALUES (1,2,'Botsing!','Ow crap!');
INSERT INTO appstoreitem (appId, pos, title, text) VALUES (2,1,'Vroem Vroem','Njewm');
INSERT INTO appstoreitem (appId, pos, title, text) VALUES (3,1,'Piew Piew','I''m Hit!');
INSERT INTO appstoreitem (appId, pos, title, text) VALUES (4,1,'Terarya','Yay');
INSERT INTO appstoreitem (appId, pos, title, text) VALUES (5,1,'Vroem Vroem','Njewm');
INSERT INTO appstoreitem (appId, pos, title, text) VALUES (6,1,'Piew Piew','I hit someone, yay!');

--appfile
INSERT INTO appfile (fileId, appId, fileName, fileType) VALUES (1,1,'/Files/dxRedist2010.exe','I');
INSERT INTO appfile (fileId, appId, fileName, fileType) VALUES (2,1,'/Files/Dirt3/game.exe','U');
INSERT INTO appfile (fileId, appId, fileName, fileType) VALUES (3,2,'/Files/NfSMO/game.exe','U');
INSERT INTO appfile (fileId, appId, fileName, fileType) VALUES (4,3,'/Files/BF4/game.exe','U');
INSERT INTO appfile (fileId, appId, fileName, fileType) VALUES (5,4,'/Files/Terarya/game.exe','U');
INSERT INTO appfile (fileId, appId, fileName, fileType) VALUES (6,5,'/Files/F1/game.exe','U');
INSERT INTO appfile (fileId, appId, fileName, fileType) VALUES (7,6,'/Files/CoDMW3/game.exe','U');

--installable
INSERT INTO installable (fileId, arguments) VALUES (1,'-q -f');

--updateable
INSERT INTO updateable (fileId, localLocation, fileVersion) VALUES (2,'{gameDir}\game.exe','1.0.0.0');
INSERT INTO updateable (fileId, localLocation, fileVersion) VALUES (3,'{gameDir}\game.exe','1.0.0.0');
INSERT INTO updateable (fileId, localLocation, fileVersion) VALUES (4,'{gameDir}\game.exe','1.0.0.0');
INSERT INTO updateable (fileId, localLocation, fileVersion) VALUES (5,'{gameDir}\game.exe','1.0.0.0');
INSERT INTO updateable (fileId, localLocation, fileVersion) VALUES (6,'{gameDir}\game.exe','1.0.0.0');
INSERT INTO updateable (fileId, localLocation, fileVersion) VALUES (7,'{gameDir}\game.exe','1.0.0.0');

--steam_user
INSERT INTO steam_user (userId, userName, passwordHash, balance) VALUES (1,'GabeN','ASRAFGHTIJZEWD56W6465F8HB',1000.00);
INSERT INTO steam_user (userId, userName, passwordHash, balance) VALUES (2,'rickRongen','WEIUGEGUGOWD56W6465F8HB',10.00);
INSERT INTO steam_user (userId, userName, passwordHash, balance) VALUES (3,'JimboLul','A5WSDF568356W6465F8HB',-1000.00);

--bought
INSERT INTO bought (userId, appId) VALUES (1,1);
INSERT INTO bought (userId, appId) VALUES (1,2);
INSERT INTO bought (userId, appId) VALUES (1,3);
INSERT INTO bought (userId, appId) VALUES (2,5);
INSERT INTO bought (userId, appId) VALUES (2,6);
INSERT INTO bought (userId, appId) VALUES (3,1);
INSERT INTO bought (userId, appId) VALUES (3,2);

--friend
INSERT into friend (userId, friendUserId, since) VALUES (1,2,TO_DATE('20110911','YYYYMMDD'));
INSERT into friend (userId, friendUserId, since) VALUES (2,1,TO_DATE('20110911','YYYYMMDD'));
INSERT into friend (userId, friendUserId, since) VALUES (1,3,TO_DATE('20120612','YYYYMMDD'));
INSERT into friend (userId, friendUserId, since) VALUES (3,1,TO_DATE('20120612','YYYYMMDD'));

--chatMessage

INSERT INTO chatMessage (sendUserId, targetUserId, send, message) VALUES (1,2,TO_DATE('20110912-12:12:25','YYYYMMDD-HH:MI:SS'),'Heyo');
INSERT INTO chatMessage (sendUserId, targetUserId, send, message) VALUES (2,1,TO_DATE('20110912-12:12:48','YYYYMMDD-HH:MI:SS'),'Hi');

--usercomment
INSERT INTO user_comment (commentId, appId, userId, text) VALUES (1,1,2,'Wow I raced so hard!');
INSERT INTO user_comment (commentId, appId, userId, text) VALUES (2,1,3,'Hah  ik was sneller :p!');
INSERT INTO user_comment (commentId, appId, userId, text) VALUES (3,2,1,'I like this one!');
INSERT INTO user_comment (commentId, appId, userId, text) VALUES (4,4,3,'LOLOLOL!');

--achievement
INSERT INTO achievement (appId, achievementId, title, description, icon) VALUES (1,1,'Roll Over!','Drift so hard your car rolls over','/icons/dirt3/rollover.jpg');
INSERT INTO achievement (appId, achievementId, title, description, icon) VALUES (1,2,'Top Speed!','Drive over 300Km/h','/icons/dirt3/topspeed.jpg');
INSERT INTO achievement (appId, achievementId, title, description, icon) VALUES (3,3,'Betrayer!','Shoot a teammate','/icons/bf4/betrayer.jpg');
INSERT INTO achievement (appId, achievementId, title, description, icon) VALUES (3,4,'Sniper!','Shoot someone at at least 100m distance','/icons/dirt3/sniper.jpg');

--user_achievement
INSERT into user_achievement (userId, achievementId, dateTime) VALUES (1,1,TO_DATE('20111226','YYYYMMDD'));
INSERT into user_achievement (userId, achievementId, dateTime) VALUES (2,1,TO_DATE('20120302','YYYYMMDD'));
INSERT into user_achievement (userId, achievementId, dateTime) VALUES (3,1,TO_DATE('20120303','YYYYMMDD'));
INSERT into user_achievement (userId, achievementId, dateTime) VALUES (2,3,TO_DATE('20111226','YYYYMMDD'));

COMMIT;