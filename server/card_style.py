import mysql.connector
from flask import Flask, request, jsonify, Blueprint
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

        print("Token ID: ", tokenId)
        print("Target Card Style ID: ", targetCardStyleId)
        print("Target Character Type: ", targetCharacterType)

        haveItem = HaveCardStyle(tokenId, targetCardStyleId)
        print("haveItem: ", haveItem)

        if targetCharacterType == "6":
            targetCharacterType = "0"
        if haveItem or (targetCardStyleId >= 55 and targetCardStyleId <= 60):
            unselectSkinId = FindEquippedCardStyle(tokenId, targetCharacterType)
            print("target unselect skin id: ", unselectSkinId)
            if unselectSkinId:
                unEquipSuccess = UnEquipCardStyle(tokenId, unselectSkinId)
                if not unEquipSuccess:
                    print("Failed to unequip skin")
                    return False
                print("Succeeded to unequip skin")
            if targetCardStyleId < 55:
                equipSuccess = UpdateEquipStatus(tokenId, targetCardStyleId)
            else:
                equipSuccess = True
            if equipSuccess:
                print("Successfully equipped skin")
                return jsonify(status="200001")
            else:
                print("Failed to equip skin")
                return jsonify(status="200021")
        else:
            print("User doesn't have this item in inventory")
            return jsonify(status="200022")
    except Exception as e:
        traceback.print_exc()
        error_message = "Internal Server Error"
        response = {"error": str(e) if str(e) else error_message}
        return jsonify(response), 500
    
def HaveCardStyle(tokenId, targetCardStyleId):
    try:
        conn = func.create_mysql_connection()
        if not conn:
            print("Failed to connect to the database.")
            return False
        
        print("Succesfully connected to server")
        
        cursor = conn.cursor()
        cursor.execute(
            "SELECT acs.card_style_id "
            "FROM account a "
            "JOIN account_card_style acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.card_style_id = %s ",
            # "LIMIT 1", 
            (tokenId, targetCardStyleId,))
        
        result = cursor.fetchone()
        print("result: ", result)

        if result is not None:
            print("User has this item")
            conn.close()
            return True
        else:
            print("User doesn't have this item")
            conn.close()
            return False
        
    except Exception as e:
        print("Error in HaveCardStyle:", e)
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
        print("Successfully equipped card style")
        return True     

    except Exception as e:
        print("Error in EquipCardStyle")
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
        print("Successfully unequipped card style")
        return True
    except Exception as e:
        print("Error in UnEquipCardStyle")
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
            print("equippedSkin: ", equippedSkin)
            conn.close()
            return equippedSkin
        else:
            print("No equipped skin")
            conn.close()
            return None
        
    except Exception as e:
        print("Error in FindEquippedCardStyle")
        conn.close()
        return None
        
