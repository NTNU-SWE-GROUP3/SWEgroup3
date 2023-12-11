import mysql.connector
from flask import Flask, request, jsonify, Blueprint
import func
import traceback
from flask import current_app

card_style = Blueprint('card_style', __name__, url_prefix='/card_style')

@card_style.route("/equip_card_style", methods=['POST'])
def EquipCardStyle():
    print("Hello")
    try:
        tokenId = request.form.get("tokenId")
        targetCardStyleId = request.form.get("targetCardStyleId")

        print("Token ID: ", tokenId)
        print("Target Card Style ID: ", targetCardStyleId)
        # current_app.logger("Token ID: ", tokenId)
        # current_app.logger("Target Card Style ID: ", targetCardStyleId)

        haveItem = HaveCardStyle(tokenId, targetCardStyleId)
        print("haveItem: ", haveItem)

        if haveItem:
            equipSuccess = UpdateEquipStatus(tokenId, targetCardStyleId)
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
    

            