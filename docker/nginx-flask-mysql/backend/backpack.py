import mysql.connector

class BPManager:
    def __init__(self, database='swegroup3game', host="db", user="root", password_file=None):
        pf = open(password_file, 'r')
        self.connection = mysql.connector.connect(
            user = user,
            password = pf.read(),
            host = host,
            database = database,
            auth_plugin = 'mysql_native_password'
        )
        pf.close()
        self.cursor = self.connection.cursor()

    def FindaccountID(self, accountID):
        insertStmt = (
                "SELECT a.id FROM account a "
                "WHERE a.id = %s "
                "LIMIT 1"
            )
        data = (accountID,)
        self.cursor.execute(insertStmt, data)
        rec = []
        for c in self.cursor:
            rec.append(c[0])
        if(bool(rec)):
            return rec[0]
        else:
            return None
        
    def HaveCardStyle(self, accountID, cardStyleID):
        account_id = self.FindaccountID(accountID)
        if(account_id is None):
            return False #account ID not found
        selectInventoryStmt = (
            "SELECT 1 FROM account_card_style "
            "WHERE account_id = %s AND cardStyle = %s "
            "LIMIT 1"
        )
        inventoryData = (account_id, cardStyleID)
        self.cursor.execute(selectInventoryStmt, inventoryData)

        return bool(list(self.cursor))
    
    def EquipCardStyle(self, accountID, cardStyleID):
        account_id = self.FindaccountID(accountID)
        if(account_id is None):
            return False #account ID not found
        update_stmt = (
            "UPDATE account_card_style "
            "SET equip_status = 1"
            "WHERE account_id = %s AND card_style_id = %s "
        )

        updateData = (account_id, cardStyleID)
        self.cursor.execute(update_stmt, updateData)
        self.connection.commit()

        return True
    
    def SellCardStyle(self, accountID, targetCardStyleID):
        account_id = self.FindaccountID(accountID)
        if(account_id is None):
            return False #account ID not found
        delete_stmt = (
            "DELETE FROM account_card_style "
            'WHERE account_id = %s AND card_style_id = %s'
        )
        data = (accountID, targetCardStyleID)
        self.cursor.execute(delete_stmt, data)
        self.connection.commit()

