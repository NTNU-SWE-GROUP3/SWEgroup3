import mysql.connector

class DBManager:
    def __init__(self, database='swegroup3game', host="db", user="root", password_file=None):
        pf = open(password_file, 'r')
        self.connection = mysql.connector.connect(
            user=user,
            password=pf.read(),
            host=host, # name of the mysql service as set in the docker compose file
            database=database,
            auth_plugin='mysql_native_password'
        )
        pf.close()
        self.cursor = self.connection.cursor()



    def AccountExist(self, accountName):
        insert_stmt = (
            "SELECT count(*) FROM account a "
            "WHERE a.name = %s LIMIT 1"
        )
        data = (accountName,)   # it have to be tuple style here.
        self.cursor.execute(insert_stmt, data)
        rec = []
        for c in self.cursor:
            rec.append(c[0])
        return True if rec[0] == 1 else False



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
        rec = []
        for c in self.cursor:
            rec.append(c[0])
        if(bool(rec)):
            return rec[0]
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
        self.connection.commit()



    def SetTokenIdAndValidity(self, accountId, tokenId, tokenValidity):
        self.cursor.execute(
            'UPDATE	account a SET a.token_id = %s, token_validity = %s WHERE a.id = %s',
            (tokenId, tokenValidity, accountId))
        self.connection.commit()


