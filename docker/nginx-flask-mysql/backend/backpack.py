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
        
    ############## CARD STYLES #################

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
    
    ############## SKILL CARD #################

    def GetSkillStyle(self, token_id, skill_id):
        #account_id = int(token_id)
        current_app.logger.info("bp acc id: ",  token_id)
        current_app.logger.info("bp skill id: ", skill_id)

         # Check if skill_id is not None before converting to int
        if skill_id is not None:
            skill_id = int(skill_id)
        else:
            # Handle the case where skill_id is None
            return -1 
        #skill_id = int(skill_id)
        
        selectInventoryStmt = (
            "SELECT acs.skill_id "
            "FROM account a "
            "JOIN account_skill acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.skill_id = %s "
        )
        # current_app.logger.info(selectInventoryStmt)
        inventoryData = (token_id, skill_id)
        self.cursor.execute(selectInventoryStmt, inventoryData)
        rec = []
        for c in self.cursor:
            rec.append(c[0]) 
        if(bool(rec)):
            current_app.logger.info("rec : " , rec)
            return rec[0]
        else:
            return -1
        
        
    def EquipSkillStyle(self, token_id, skillId):
        # account_id = self.FindAccountId(accountID)
        # if(account_id == -1):
        #     return False #account ID not found
        update_stmt = (
            "UPDATE account_skill acs "
            "JOIN account a ON a.id = acs.account_id "
            "SET acs.equip_status = 1 "
            "WHERE a.token_id = %s AND acs.skill_id = %s"
        )

        updateData = (token_id, skillId)
        self.cursor.execute(update_stmt, updateData)
        self.connection.commit()

        return True

    def DisplaySkillStyle(self, token_id):
        #current_app.logger.info("input skill id: ", skill_id)
         # Create a comma-separated string of placeholders for the IN clause
        #placeholders = ', '.join(['%s'] * len(skill_id))

        select_stmt = (
            "SELECT acs.skill_id "
            "FROM account a "
            "JOIN account_skill acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s"
        )

        # Combine the token_id and skill_id into a single tuple
        #params = (token_id,) + tuple(skill_id)

        self.cursor.execute(select_stmt, (token_id,))

        # Fetch all the results
        results = self.cursor.fetchall()

        # Extract and return the skill_id values
        skill_style_ids = [result[0] for result in results]
        current_app.logger.info("result: ", skill_style_ids)

        return skill_style_ids