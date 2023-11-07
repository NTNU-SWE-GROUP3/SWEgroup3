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



    def AccountExist(self, accountName, accountPassword):
        self.cursor.execute('SELECT count(*) FROM account a WHERE a.name = %s AND a.password = %s LIMIT 1',
            (accountName, accountPassword))
        rec = []
        for c in self.cursor:
            rec.append(c[0])
        return rec



    def AccountLogin(self, accountName, accountPassword, tokenId, tokenValidity):
        self.cursor.execute(
            'UPDATE	account a SET a.token_id = %s, token_validity = %s WHERE a.name = %s AND a.password = %s',
            (tokenId, tokenValidity, accountName, accountPassword))
        self.connection.commit()


