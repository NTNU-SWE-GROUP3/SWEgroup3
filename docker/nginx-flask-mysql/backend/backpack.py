import mysql.connector
from flask import current_app

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

    def FindAccountId(self, accountName):
        insertStmt = (
            "SELECT a.id FROM account a "
            "WHERE a.name = %s "
            "LIMIT 1"
        )
        data = (accountName,)
        self.cursor.execute(insertStmt, data)
    #if tokenid not even exist, -1 will be returned
    def GetTokenExpiredTime(self, tokenid):
        insertStmt = (
            "SELECT a.token_validity FROM account a "
            "WHERE a.token_id = %s LIMIT 1"
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
        
    def HaveCardStyle(self, account_id, card_style_id):
        # current_app.logger.info(accountID)
        # account_id = self.FindAccountId(accountID)
        # current_app.logger.info(account_id)
        # if(account_id == -1):
        #     return False #account ID not found
        account_id = int(account_id)
        card_style_id = int(card_style_id)
        current_app.logger.info("bp acc id: ",  account_id)
        current_app.logger.info("bp card style id: ", card_style_id)
        selectInventoryStmt = (
            # "SELECT 1 FROM account_card_style "
            # "WHERE a.account_id = %s AND a.cardStyle = %s "
            # "LIMIT 1"
            "SELECT acs.card_style_id FROM account_card_style acs "
            "WHERE acs.account_id = %s AND acs.card_style_id = %s "
            "LIMIT 1"
        )
        current_app.logger.info(selectInventoryStmt)
        inventoryData = (account_id, card_style_id)
        self.cursor.execute(selectInventoryStmt, inventoryData)
        result = self.cursor.fetchone()
        # return bool(list(self.cursor))
        current_app.logger.info("result: %s",result)
        return bool(result)
       
    
    def EquipCardStyle(self, accountID, cardStyleID):
        # account_id = self.FindAccountId(accountID)
        # if(account_id == -1):
        #     return False #account ID not found
        update_stmt = (
            "UPDATE account_card_style "
            "SET equip_status = 1"
            "WHERE account_id = %s AND card_style_id = %s "
        )

        updateData = (accountID, cardStyleID)
        self.cursor.execute(update_stmt, updateData)

    # def FindAccountId(self, accountName):
    #     insertStmt = (
    #         "SELECT a.id FROM account a "
    #         "WHERE a.name = %s "
    #         "LIMIT 1"
    #     )
    #     data = (accountName,)
    #     self.cursor.execute(insertStmt, data)
    #     rec = []
    #     for c in self.cursor:
    #         rec.append(c[0])
    #     if(bool(rec)):
    #         return rec[0]
    #     else:
    #         return -1
        
    def GetCardStyle(self, token_id, card_style_id):
        current_app.logger.info("bp acc id: " +  token_id)
        current_app.logger.info("bp card style id: "+ card_style_id)
        selectInventoryStmt = (
            "SELECT acs.card_style_id "
            "FROM account a "
            "JOIN account_card_style acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.card_style_id = %s "
            "LIMIT 1"
        )
        # current_app.logger.info(selectInventoryStmt)
        inventoryData = (token_id, card_style_id,)
        self.cursor.execute(selectInventoryStmt, inventoryData)
        current_app.logger.info(self.cursor._executed)
        rec = []
        for c in self.cursor:
            rec.append(c[0])
        
        # current_app.logger.info("rec: ", rec)
        
        if(bool(rec)):
            return rec[0]
        else:
            return -1
       
    
    def EquipCardStyle(self, token_id, cardStyleID):
        update_stmt = (
            "UPDATE account_card_style acs "
            "JOIN account a ON a.id = acs.account_id "
            "SET acs.equip_status = 1 "
            "WHERE a.token_id = %s AND acs.card_style_id = %s"
        )

        updateData = (token_id, cardStyleID,)
        self.cursor.execute(update_stmt, updateData)
        current_app.logger.info(self.cursor._executed)
        self.connection.commit()

        return True
    
    def SellCardStyle(self, accountID, targetCardStyleID):
        account_id = self.FindAccountId(accountID)
        if(account_id is None):
            return False #account ID not found
        delete_stmt = (
            "DELETE FROM account_card_style "
            'WHERE account_id = %s AND card_style_id = %s'
        )
        data = (accountID, targetCardStyleID)
    def DisplayCardStyle(self, token_id, cardStyleID):
        current_app.logger.info("input card style id: ", cardStyleID)
         # Create a comma-separated string of placeholders for the IN clause
        placeholders = ', '.join(['%s'] * len(cardStyleID))

        select_stmt = (
            "SELECT acs.card_style_id "
            "FROM account a "
            "JOIN account_card_style acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.card_style_id IN ({})"
        ).format(placeholders)

        # Combine the token_id and cardStyleIDs into a single tuple
        params = (token_id,) + tuple(cardStyleID)

        self.cursor.execute(select_stmt, params)

        # Fetch all the results
        results = self.cursor.fetchall()

        # Extract and return the card_style_id values
        card_style_ids = [result[0] for result in results]
        current_app.logger.info("result: ", card_style_ids)

        return card_style_ids
        # select_stmt = (
        #     "SELECT acs.card_style_id "
        #     "FROM account a "
        #     "JOIN account_card_style acs ON a.id = acs.account_id "
        #     "WHERE a.token_id = %s"
        # )
        # self.cursor.execute(select_stmt, (token_id,))

        # # Fetch all the results
        # results = self.cursor.fetchall()

        # # Extract and return the card_style_id values
        # card_style_ids = [result[0] for result in results]

        # return card_style_ids
    
    def SellCardStyle(self, token_id, targetCardStyleID):
        current_app.logger.info("bp token id: " +  token_id)
        current_app.logger.info("bp card style id: "+ targetCardStyleID)
        delete_stmt = (
            "DELETE FROM account_card_style "
            'WHERE account_id IN (SELECT id FROM account WHERE token_id = %s) AND card_style_id = %s'
        )
        data = (token_id, targetCardStyleID,)
        self.cursor.execute(delete_stmt, data)
        self.connection.commit()

