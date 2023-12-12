import mysql.connector
from flask import current_app, jsonify

class DBManager:
    def __init__(self, database='game', host="localhost", user="swegroup3", password_file=None):
        # pf = open(password_file, 'r')
        self.connection = mysql.connector.connect(
            user=user,
            # password=pf.read(),
            password='Swegroup3@12345',
            host=host, # name of the mysql service as set in the docker compose file
            database=database,
            auth_plugin='mysql_native_password',
            autocommit=True
        )
        # pf.close()
        self.cursor = self.connection.cursor()



    def AccountExist(self, accountName):
        insert_stmt = (
            "SELECT count(*) FROM account a "
            "WHERE a.name = %s LIMIT 1"
        )
        data = (accountName,)   # it have to be tuple style here.
        self.cursor.execute(insert_stmt, data)
        current_app.logger.info(self.cursor._executed)
        result = self.cursor.fetchone()
        if(result[0] >= 1):
            return True
        else:
            return False



    def EmailExist(self, accountEmail):
        insert_stmt = (
            "SELECT count(*) FROM account a "
            "WHERE a.email = %s LIMIT 1"
        )
        data = (accountEmail,)   # it have to be tuple style here.
        self.cursor.execute(insert_stmt, data)
        current_app.logger.info(self.cursor._executed)
        result = self.cursor.fetchone()
        if(result[0] >= 1):
             return True
        else:
             return False



    def AccountPasswordCheck(self, accountName, accountPassword):
        insertStmt = (
            "SELECT a.id FROM account a "
            "WHERE a.password = SHA1(CONCAT("
            "%s,"
            "(SELECT a.salt FROM account a WHERE a.name = %s))) "
            "LIMIT 1"
        )
        data = (accountPassword, accountName)
        self.cursor.execute(insertStmt, data)
        current_app.logger.info(self.cursor._executed)
        result = self.cursor.fetchone()
        if(result):
             account_id = result[0]
             return account_id
        else:
             return -1


    # Use this when sign up and reset password(forget password)
    def GenerateSalt(self, accountId):
        # SHA1 generates a hexadecimal code,
        # so I specified 32 digits,
        # which is the same as 2^128.(large enough?)
        insert_stmt = (
            "UPDATE account a "
            "SET a.salt = SUBSTRING(SHA1(RAND()), 1, 32) "
            "WHERE a.id = %s"
        )
        data = (accountId,)
        self.cursor.execute(insert_stmt, data)
        current_app.logger.info(self.cursor._executed)
        self.connection.commit()



    def SetTokenIdAndValidity(self, accountId, tokenId, tokenValidity):
        self.cursor.execute(
            'UPDATE	account a SET a.token_id = %s, a.token_validity = %s WHERE a.id = %s',
            (tokenId, tokenValidity, accountId))
        current_app.logger.info(self.cursor._executed)
        self.connection.commit()



    def SetNewAccount(self, accountName, accountEmail, accountPassword):
        insert_stmt = (
            "INSERT INTO account(name, email, password, token_id, token_validity, salt) "
            "VALUES(%s, %s, %s, NULL, NULL, SUBSTRING(SHA1(RAND()), 1, 32))"
        )
        data = (accountName, accountEmail, accountPassword,)
        self.cursor.execute(insert_stmt, data)
        current_app.logger.info(self.cursor._executed)
        self.connection.commit()
        
    
    
    def InitNewAccountData(self, accountId):
        insert_stmt = (
            "INSERT INTO account_data(account_id, nickname, level, experience, `rank`, total_match, total_win, ranked_winning_streak, ranked_XP, coin) "
            "VALUES(%s, %s, %s, %s, %s, %s, %s, %s, %s, %s)"
        )
        data = (accountId, "Guest", "0", "0", "Not Ranked", "0", "0", "0", "0", "3000",)
        self.cursor.execute(insert_stmt, data)
        current_app.logger.info(self.cursor._executed)
        self.connection.commit()



    def UpdateNewAccountPassword(self, accountId):
        insert_stmt = (
            "UPDATE account a "
            "SET a.password = SHA1(CONCAT(a.password, a.salt)) "
            "WHERE a.id = %s"
        )
        data = (accountId,)
        self.cursor.execute(insert_stmt, data)
        current_app.logger.info(self.cursor._executed)
        self.connection.commit()



    def FindAccountId(self, accountName):
            insertStmt = (
                "SELECT a.id FROM account a "
                "WHERE a.name = %s "
                "LIMIT 1"
            )
            data = (accountName,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return -1

    def AcountEmailCheck(self, account_id, input_email):
        insertStmt = (
            "SELECT a.email FROM account a "
            "WHERE a.id = %s LIMIT 1"
        )
        data = (account_id,)
        self.cursor.execute(insertStmt, data)
        result = self.cursor.fetchone()

        if result:
            db_email = result[0]
            return True if db_email == input_email else False
        else:
            return False

    def SetVerifyCodeAndValidity(self, accountId, verifycode, expiretime):
        self.cursor.execute(
            'UPDATE	account a SET a.verify_code = %s, a.expiration_time = %s WHERE a.id = %s',
            (verifycode, expiretime, accountId))
        self.connection.commit()

    def CheckVerifyCode(self, accountId, input_verifycode):
        insertStmt = (
            "SELECT a.verify_code FROM account a "
            "WHERE a.id = %s LIMIT 1"
        )
        data = (accountId,)
        self.cursor.execute(insertStmt, data)
        result = self.cursor.fetchone()

        if result:
            db_verifycode = result[0]
            return True if db_verifycode == input_verifycode else False
        else:
            return False

    def GetVerifyCodeExpiredTime(self, accountId):
        insertStmt = (
            "SELECT a.expiration_time FROM account a "
            "WHERE a.id = %s LIMIT 1"
        )
        data = (accountId,)
        self.cursor.execute(insertStmt, data)
        rec = []
        for c in self.cursor:
            rec.append(c[0])
        if(bool(rec)):
            return rec[0]
        else:
            return -1

    #if tokenid not even exist, -1 will be returned
    def GetTokenExpiredTime(self, tokenid):
        insertStmt = (
            "SELECT a.token_validity FROM account a "
            "WHERE a.token_id = %s LIMIT 1"
        )
        data = (tokenid,)
        self.cursor.execute(insertStmt, data)
        current_app.logger.info(self.cursor._executed)
        result = self.cursor.fetchone()
        if(result):
             return result[0]
        else:
             return -1

    def ReloadChangePassword(self, accountId, newpassword):
        insert_stmt = (
            "UPDATE account a "
            "SET a.password = SHA1(CONCAT(%s, a.salt)) "
            "WHERE a.id = %s"
        )
        data = (newpassword, accountId)
        self.cursor.execute(insert_stmt, data)
        self.connection.commit()
        return insert_stmt


    def sqlInjectionTest(self, accountName):
            insertStmt = (
                "SELECT * FROM account a "
                "WHERE a.name = %s"
            )
            data = (accountName,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            return self.cursor.fetchall()


    #>>>>>>>>look up data via token<<<<<<<<<

    def GetUserNickname(self, tokenid):
            insertStmt = (
                "SELECT ad.nickname "
                "FROM account a "
                "JOIN account_data ad ON a.id = ad.account_id "
                "WHERE a.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return "guest"

    def GetUserEmail(self, tokenid):
            insertStmt = (
                "SELECT a.email FROM account a "
                "WHERE a.token_id = %s "
                "LIMIT 1"
            )
            data = (tokenid,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return -1

    def GetUsertotalgame(self, tokenid):
            insertStmt = (
                "SELECT ad.total_match "
                "FROM account a "
                "JOIN account_data ad ON a.id = ad.account_id "
                "WHERE a.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return 0

    def GetUsertotalwin(self, tokenid):
            insertStmt = (
                "SELECT ad.total_win "
                "FROM account a "
                "JOIN account_data ad ON a.id = ad.account_id "
                "WHERE a.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return 0

    def GetUserrank(self, tokenid):
            insertStmt = (
                "SELECT ad.rank "
                "FROM account a "
                "JOIN account_data ad ON a.id = ad.account_id "
                "WHERE a.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return "Not Ranked"

    def GetUsercoin(self, tokenid):
            insertStmt = (
                "SELECT ad.coin "
                "FROM account a "
                "JOIN account_data ad ON a.id = ad.account_id "
                "WHERE a.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return 0

    def GetUserlevel(self, tokenid):
            insertStmt = (
                "SELECT ad.level "
                "FROM account a "
                "JOIN account_data ad ON a.id = ad.account_id "
                "WHERE a.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(insertStmt, data)
            current_app.logger.info(self.cursor._executed)
            rec = []
            for c in self.cursor:
                rec.append(c[0])
            if(bool(rec)):
                return rec[0]
            else:
                return 0
            
    ##>>Dont Destroy<<
    def GetCardInfo(self):
            query = (
                "SELECT * FROM card_style"
            )
            self.cursor.execute(query)
            current_app.logger.info(self.cursor._executed)
            rows = self.cursor.fetchall()
            card_info = []
            for row in rows:
                card_info.append(row)
            return (card_info)
        
    def GetSkillInfo(self):
            query = (
                "SELECT * FROM skill"
            )
            self.cursor.execute(query)
            current_app.logger.info(self.cursor._executed)
            rows = self.cursor.fetchall()
            card_info = []
            for row in rows:
                card_info.append(row)
            return (card_info)
        
        
    def GetSkillData(self, tokenid):
            query = (
                "SELECT account_skill.skill_id, account_skill.equip_status "
                "FROM account_skill "
                "JOIN account ON account_skill.account_id = account.id "
                "WHERE account.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(query, data)
            current_app.logger.info(self.cursor._executed)
            rows = self.cursor.fetchall()
            skill_data = []
            for row in rows:
                skill_data.append(row)
            return (skill_data)
        
        

    def GetStyleData(self, tokenid):
            query = (
                "SELECT account_card_style.card_style_id, account_card_style.equip_status "
                "FROM account_card_style "
                "JOIN account ON account_card_style.account_id = account.id "
                "WHERE account.token_id = %s"
            )
            data = (tokenid,)
            self.cursor.execute(query, data)
            current_app.logger.info(self.cursor._executed)
            rows = self.cursor.fetchall()
            style_data = []
            for row in rows:
                style_data.append(row)
            return (style_data)
        
        
        
        
