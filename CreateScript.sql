DROP TABLE user_achievement;
DROP TABLE achievement;
DROP TABLE user_comment;
DROP TABLE chatmessage;
DROP TABLE friend;
DROP TABLE bought;
DROP TABLE steam_user;
DROP TABLE updateable;
DROP TABLE installable;
DROP TABLE appfile;
DROP TABLE appstoreitem;
DROP TABLE app_pack;
DROP TABLE pack;
DROP TABLE pictures;
DROP TABLE app;
DROP TABLE categorie;

CREATE TABLE categorie(
  categorieId NUMBER(5) NOT NULL,
  categorieName VARCHAR2(32) NOT NULL UNIQUE,
  superCategorie NUMBER(5),
  CONSTRAINT pk_categorie PRIMARY KEY (categorieId)
);

CREATE TABLE app(
  appId NUMBER(6) NOT NULL,
  categorieId NUMBER(5) NOT NULL,
  appName VARCHAR2(32) NOT NULL,
  CONSTRAINT pk_app PRIMARY KEY (appId),
  CONSTRAINT fk_app_catId FOREIGN KEY (categorieId) REFERENCES categorie (categorieId)
);

CREATE TABLE pictures(
  appId NUMBER(6) NOT NULL,
  picId NUMBER(6) NOT NULL UNIQUE,
  CONSTRAINT pk_pictures PRIMARY KEY (appId, picId)
);

CREATE TABLE pack(
  packId NUMBER(6) NOT NULL,
  packName VARCHAR2(32) NOT NULL,
  description VARCHAR2(256) NOT NULL,
  price NUMBER(4,2) NOT NULL,
  discount NUMBER(3,3) NOT NULL,
  CONSTRAINT pk_pack PRIMARY KEY(packId)
);

CREATE TABLE app_pack(
  packId NUMBER(6) NOT NULL,
  appId NUMBER(6) NOT NULL,
  CONSTRAINT pk_app_pack PRIMARY KEY (packId, appId)
);

CREATE TABLE appstoreitem(
  appId NUMBER(6) NOT NULL,
  pos NUMBER(3) NOT NULL,
  title VARCHAR2(32) NOT NULL,
  text CLOB NOT NULL,
  CONSTRAINT pk_appstoreitem PRIMARY KEY (appId, pos),
  CONSTRAINT fk_appstoretitem_appId FOREIGN KEY (appId) REFERENCES app(appId)
);

CREATE TABLE appfile(
  fileId NUMBER(8) NOT NULL UNIQUE,
  appId NUMBER(6) NOT NULL,
  fileName VARCHAR2(256) NOT NULL,
  fileType VARCHAR2(1) DEFAULT 'U' NOT NULL ,--U=updateable I=installable
  CONSTRAINT pk_appfile PRIMARY KEY (fileId, appId),
  CONSTRAINT fk_appfile_appId FOREIGN KEY (appId) REFERENCES app(appId),
  CONSTRAINT ck_appfile_filetype CHECK(fileType = 'U' OR fileType = 'I')
);

CREATE TABLE installable(
  fileId NUMBER(8) NOT NULL,
  arguments VARCHAR2(64) NOT NULL,
  CONSTRAINT pk_installable PRIMARY KEY (fileId),
  CONSTRAINT fk_installable_fileId FOREIGN KEY (fileId) REFERENCES appfile(fileId)
);

CREATE TABLE updateable(
  fileId NUMBER(8) NOT NULL,
  localLocation VARCHAR2(256) NOT NULL,
  fileVersion VARCHAR2(8) NOT NULL,
  CONSTRAINT pk_updateable PRIMARY KEY (fileId),
  CONSTRAINT fk_updateable_fileId FOREIGN KEY (fileId) REFERENCES appfile (fileId)
);

CREATE TABLE steam_user(
  userId NUMBER(10) NOT NULL,
  userName VARCHAR2(32) NOT NULL UNIQUE,
  passwordHash VARCHAR2(256) NOT NULL,
  balance NUMBER(6,2) DEFAULT 0.00,
  CONSTRAINT pk_userId PRIMARY KEY (userId)
);

CREATE TABLE bought(
  userId NUMBER(10) NOT NULL,
  appId NUMBER(6) NOT NULL,
  CONSTRAINT pk_bought PRIMARY KEY (userId, appId)
);

CREATE TABLE friend(
  userId NUMBER(10) NOT NULL,
  friendUserId NUMBER(10) NOT NULL,
  since DATE NOT NULL,
  CONSTRAINT pk_friend PRIMARY KEY (userId, friendUserId)
);

CREATE TABLE chatMessage(
  sendUserId NUMBER(10) NOT NULL,
  targetUserId NUMBER(10) NOT NULL,
  send DATE NOT NULL,
  message VARCHAR2(1024),
  CONSTRAINT pk_chatMessage PRIMARY KEY (sendUserId, targetUserId, send),
  CONSTRAINT fk_chatMessage_sendUserId FOREIGN KEY (sendUserId) REFERENCES steam_user(userId),
  CONSTRAINT fk_chatMessage_targetUserId FOREIGN KEY (targetUserId) REFERENCES steam_user(userId)
);

CREATE TABLE user_comment(
  commentId NUMBER(16) NOT NULL,
  appId NUMBER(8) NOT NULL,
  userId NUMBER(10) NOT NULL,
  text VARCHAR2(2048) NOT NULL,
  CONSTRAINT pk_user_comment PRIMARY KEY (commentId, appId),
  CONSTRAINT fk_user_comment_userId FOREIGN KEY (userId) REFERENCES steam_user(userId),
  CONSTRAINT fk_user_comment_appId FOREIGN KEY (appId) REFERENCES app(appId)
);

CREATE TABLE achievement(
  appId NUMBER(8) NOT NULL,
  achievementId NUMBER(8) NOT NULL UNIQUE,
  title VARCHAR2(32) NOT NULL,
  description VARCHAR2(256) NOT NULL,
  icon VARCHAR2(256) NOT NULL,--url to icon
  CONSTRAINT pk_achievement PRIMARY KEY (appId, achievementId),
  CONSTRAINT fk_achievement_appId FOREIGN KEY (appId) REFERENCES app(appId)
);

CREATE TABLE user_achievement(
  userId NUMBER(10) NOT NULL,
  achievementId NUMBER(8) NOT NULL,
  dateTime DATE NOT NULL,
  CONSTRAINT pk_user_achievement PRIMARY KEY (userId, achievementId),
  CONSTRAINT fk_user_ach_userId FOREIGN KEY (userId) REFERENCES steam_user(userId),
  CONSTRAINT fk_user_ach_achievementId FOREIGN KEY (achievementId) REFERENCES achievement(achievementId)
);

--Categorie name=>categorieName
--App
--Pictures
--Pack name=>packName
--App_pack
--appstoreitem
--appfile type=>fileType type enum=>varchar2(1)
--installable
--updateable
--User
--Bought
--Friend
--Chatmessage
--usercomment comment=>text
--achievement
--user_achievement
