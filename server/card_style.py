import mysql.connector
from flask import Flask, request, jsonify, Blueprint,current_app
import func
import traceback

card_style = Blueprint('card_style', __name__, url_prefix='/card_style')

''' =========================
equip skin
input
    token ID
    target card style
output
    status
        200001 : equip success
        200021 : equip failure
        200022 : Item doesn't exist in inventory
========================= '''
@card_style.route("/equip_card_style", methods=['POST'])
def EquipCardStyle():
    try:
        tokenId = request.form.get("tokenId")
        targetCardStyleId = request.form.get("targetCardStyleId")
        targetCharacterType = request.form.get("targetCharacterType")

        current_app.logger.info("Token ID: ", tokenId)
        current_app.logger.info("Target Card Style ID: ", targetCardStyleId)
        current_app.logger.info("Target Character Type: ", targetCharacterType)

        haveItem = HaveCardStyle(tokenId, targetCardStyleId)
        current_app.logger.info("haveItem: ", haveItem)

        if targetCharacterType == "6":
            targetCharacterType = "0"
        if haveItem == True or (targetCardStyleId >= 55 and targetCardStyleId <= 60):
            unselectSkinId = FindEquippedCardStyle(tokenId, targetCharacterType)
            current_app.logger.info("target unselect skin id: ", unselectSkinId)
            if unselectSkinId:
                unEquipSuccess = UnEquipCardStyle(tokenId, unselectSkinId)
                if not unEquipSuccess:
                    current_app.logger.info("Failed to unequip skin")
                    return False
                current_app.logger.info("Succeeded to unequip skin")
            if targetCardStyleId < 55:
                equipSuccess = UpdateEquipStatus(tokenId, targetCardStyleId)
            else:
                equipSuccess = True
            if equipSuccess:
                current_app.logger.info("Successfully equipped skin")
                return jsonify(status="200001")
            else:
                current_app.logger.info("Failed to equip skin")
                return jsonify(status="200021")
        else:
            current_app.logger.info("User doesn't have this item in inventory")
            return jsonify(status="200022")
    except Exception as e:
        traceback.current_app.logger.info_exc()
        error_message = "Internal Server Error"
        response = {"error": str(e) if str(e) else error_message}
        return jsonify(response), 500
    
def HaveCardStyle(tokenId, targetCardStyleId):
    try:
        conn = func.create_mysql_connection()
        if not conn:
            current_app.logger.info("Failed to connect to the database.")
            return False
        
        current_app.logger.info("Succesfully connected to server")
        
        cursor = conn.cursor()
        cursor.execute(
            "SELECT acs.card_style_id "
            "FROM account a "
            "JOIN account_card_style acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.card_style_id = %s ",
            # "LIMIT 1", 
            (tokenId, targetCardStyleId,))
        
        result = cursor.fetchone()
        current_app.logger.info("result: ", result)

        if result is not None:
            current_app.logger.info("User has this item")
            conn.close()
            return True
        else:
            current_app.logger.info("User doesn't have this item")
            conn.close()
            return False
        
    except Exception as e:
        current_app.logger.info("Error in HaveCardStyle:", e)
        conn.close()
        return False
    
def UpdateEquipStatus(tokenId, targetCardStyleId):
    try:
        conn = func.create_mysql_connection()
        cursor = conn.cursor()
        cursor.execute(
            "UPDATE account_card_style acs "
            "JOIN account a ON a.id = acs.account_id "
            "SET acs.equip_status = 1 "
            "WHERE a.token_id = %s AND acs.card_style_id = %s",
            (tokenId, targetCardStyleId,)
        )
        conn.commit()
        conn.close()
        current_app.logger.info("Successfully equipped card style")
        return True     

    except Exception as e:
        current_app.logger.info("Error in EquipCardStyle")
        conn.close()
        return False
    
def UnEquipCardStyle(tokenId, targetCardStyleId):
    try:
        conn = func.create_mysql_connection()
        cursor = conn.cursor()
        cursor.execute(
            "UPDATE account_card_style acs "
            "JOIN account a ON a.id = acs.account_id "
            "SET acs.equip_status = 0 "
            "WHERE a.token_id = %s AND acs.card_style_id = %s",
            (tokenId, targetCardStyleId,)
        )
        conn.commit()
        conn.close()
        current_app.logger.info("Successfully unequipped card style")
        return True
    except Exception as e:
        current_app.logger.info("Error in UnEquipCardStyle")
        conn.close()
        return False

def FindEquippedCardStyle(tokenId, characterType):
    try:
        conn = func.create_mysql_connection()
        cursor = conn.cursor()
        cursor.execute(
            "SELECT acs.card_style_id "
            "FROM account a "
            "JOIN account_card_style acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.equip_status = 1 AND CAST(acs.card_style_id AS SIGNED) % 6 = %s",
            (tokenId, characterType,)
        )
        result = cursor.fetchone()
        if result:
            equippedSkin = result[0]
            current_app.logger.info("equippedSkin: ", equippedSkin)
            conn.close()
            return equippedSkin
        else:
            current_app.logger.info("No equipped skin")
            conn.close()
            return None
        
    except Exception as e:
        current_app.logger.info("Error in FindEquippedCardStyle")
        conn.close()
        return None
        
