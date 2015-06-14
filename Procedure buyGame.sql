CREATE OR REPLACE PROCEDURE BuyGame(usrn VARCHAR2, pids VARCHAR2) AS
  pr NUMBER;
  
  
  
  pid NUMBER;
  uid NUMBER;
  cnt NUMBER;
  
  CURSOR games IS
    SELECT appId, packId
    FROM app_pack
    WHERE packId = pid;
BEGIN
  pid := TO_NUMBER(TRIM(pids));
  --check the price and the balance
  SELECT price INTO pr
  FROM pack WHERE packId = pid;
  
  SELECT COUNT(*) INTO cnt
  FROM steam_user
  WHERE username = usrn
  AND balance > pr;
  IF(cnt<1) THEN
    RAISE_APPLICATION_ERROR(-20000,'Not enough balance or unkown user!');
  END IF;
  SELECT userid INTO uid
  FROM steam_user
  WHERE username = usrn;
  
  --remove money from user
  UPDATE steam_user
  SET balance = balance - pr
  WHERE userid = uid;
  
  FOR r IN games LOOP
    SELECT COUNT(*) INTO cnt
    FROM bought
    WHERE userid = uid
    AND appId = r.appId;
    IF(cnt<1)THEN--only new games
      INSERT INTO BOUGHT VALUES(uid,r.appId);
    END IF;
  END LOOP;
  commit;
END;
/
DELETE FROM bought
WHERE userid=1 AND appid=4;
SELECT *
FROM steam_user;
EXEC BuyGame('GabeN',5);
commit;
