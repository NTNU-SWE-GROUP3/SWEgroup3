from flask import current_app
from flask import Blueprint
from flask import Flask
from flask import jsonify
from flask import request
from flask import make_response
from db import DBManager
from backpack import BPManager
from flask import current_app
import datetime
import func
import traceback
import json

skill_style = Blueprint('skill_style', __name__, url_prefix='/skill_style')

conn = None

@skill_style.route("/display_skill_style", methods=['POST'])
def DisplaySkillStyle():
    try:
        token = request.form.get('Token')
        current_app.logger.info("tokenID: %s", token)

        skill_collection = GetSkillStyle(token)
        # result = make_response(skin_collection)
        current_app.logger.info("skillcollection: %s", skill_collection)
        
        if skill_collection is not None: 
            return jsonify(status = "400055",
                           msg = "Success",
                           skill_collection = skill_collection)
        else:
            return jsonify({"error": "Failed to fetch skill collection."}), 500

    except Exception as e:
        print("Error in display_skill_style:", e)
        return False

def GetSkillStyle(token_id):
    try:
        connection = func.create_mysql_connection()

        if not connection:
            print("Failed to connect to the database.")
            return None

        cursor = connection.cursor(dictionary=True)
        cursor.execute("SELECT acs.skill_id "
                       "FROM account a "
                       "JOIN account_skill acs ON a.id = acs.account_id "
                       "WHERE a.token_id = %s", (token_id,))

        # cursor.execute("SELECT * FROM account a JOIN account_skill acs ON a.id = acs.account_id WHERE a.token_id = %s", (token_id,) )

        response = cursor.fetchall()
        current_app.logger.info("response: %s", response)
        skill_ids = [entry['skill_id'] for entry in response]
        # print(skill_ids)

        # # Convert to JSON serializable format
        # skill_collection = make_response(skill_ids)
        connection.close()

        if skill_ids:
            print(skill_ids)
            return skill_ids
        else:
            return []

    
    except Exception as e:
        print("Error in DisplaySkillStyle:", e)
        return False
    
@skill_style.route("/check_equip_status", methods=['POST'])
def CheckEquipStatus():
    try:
        tokenId = request.form.get("tokenId")
        targetSkillId = request.form.get("targetSkillId")

        print("Token ID: ", tokenId)
        print("Target skill ID: ", targetSkillId)

        equip_stat = checkstatus(tokenId, targetSkillId)
        
        current_app.logger.info("equip STATUS ", equip_stat)
        print("equip STATUS ", equip_stat)
        if(equip_stat): #true
            return "1"
        else:
            return "0"

    except Exception as e:
        traceback.print_exc()
        error_message = "Internal Server Error"
        response = {"error": str(e) if str(e) else error_message}
        return jsonify(response), 500
    
def checkstatus(tokenId, targetSkillId):
    try:
        conn = func.create_mysql_connection()
        if not conn:
            print("Failed to connect to the database.")
            return False
        
        print("Succesfully connected to server")
        
        cursor = conn.cursor()
        cursor.execute(
            "SELECT acs.equip_status "
            "FROM account a "
            "JOIN account_skill acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.skill_id = %s ",
            # "LIMIT 1", 
            (tokenId, targetSkillId,))
        
        result = cursor.fetchone()

        current_app.logger.info("\tcheck status result: ", result)

        if result is 1:
            print("status : item is equipped")
            conn.close()
            return True
        elif result is 0:
            print("status : item not equipped, equipping...")
            conn.close()
            return False
        
    except Exception as e:
        print("Error in checkequipstatus:", e)
        conn.close()
        return False
    
  
    
    
@skill_style.route("/toggle_equip_status", methods=['POST'])
def ToggleEquipStatus():
    try:
        tokenId = request.form.get("tokenId")
        targetSkillId = request.form.get("targetSkillId")

        print("Token ID: ", tokenId)
        print("Target skill ID: ", targetSkillId)

        equipped_count = count_equipped_skills(tokenId)
        print(equipped_count)
        if equipped_count >= 3:
            current_status = checkstatus(tokenId, targetSkillId)
            if current_status:
                new_status = not current_status  # Toggle the current status
                update_status_success = updateEquipStatus(tokenId, targetSkillId, new_status)

                if update_status_success:
                    return "1" if new_status else "0"  # Return the new equip status
                else:
                    return "Error updating equip status", 500
            else:
                return "2"
        else :
            current_status = checkstatus(tokenId, targetSkillId)
            new_status = not current_status  # Toggle the current status

            # Print the current and new equip status for debugging
            current_app.logger.info("Current Equip Status: ", current_status)
            current_app.logger.info("New Equip Status: ", new_status)

            # Update the equip status in the database
            update_status_success = updateEquipStatus(tokenId, targetSkillId, new_status)

            if update_status_success:
                return "1" if new_status else "0"  # Return the new equip status
            else:
                return "Error updating equip status", 500

    except Exception as e:
        traceback.print_exc()
        error_message = "Internal Server Error"
        response = {"error": str(e) if str(e) else error_message}
        return jsonify(response), 500


def updateEquipStatus(tokenId, targetSkillId, new_status):
    try:
        conn = func.create_mysql_connection()
        if not conn:
            print("Failed to connect to the database.")
            return False

        print("Successfully connected to server")

        cursor = conn.cursor()

        # Update the equip_status in the database
        cursor.execute(
            "UPDATE account_skill "
            "SET equip_status = %s "
            "WHERE account_id = (SELECT id FROM account WHERE token_id = %s) "
            "AND skill_id = %s",
            (int(new_status), tokenId, targetSkillId)
        )

        conn.commit()
        conn.close()
        return True

    except Exception as e:
        print("Error in updateEquipStatus:", e)
        conn.close()
        return False

def checkstatus(tokenId, targetSkillId):
    try:
        conn = func.create_mysql_connection()
        if not conn:
            print("Failed to connect to the database.")
            return False

        print("Successfully connected to the server")

        cursor = conn.cursor()
        cursor.execute(
            "SELECT acs.equip_status "
            "FROM account a "
            "JOIN account_skill acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.skill_id = %s",
            (tokenId, targetSkillId,)
        )

        result = cursor.fetchone()
        print("result: ", result)

        if result is not None:
            equip_status = result[0]
            print("equip_status: ", equip_status)
            conn.close()
            return equip_status == 1

    except Exception as e:
        print("Error in checkstatus:", e)

    conn.close()
    return False

def count_equipped_skills(tokenId):
    try:
        conn = func.create_mysql_connection()
        if not conn:
            print("Failed to connect to the database.")
            return 0

        print("Successfully connected to the server")

        cursor = conn.cursor()
        cursor.execute(
            "SELECT COUNT(*) "
            "FROM account_skill acs "
            "JOIN account a ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.equip_status = 1",
            (tokenId,)
        )

        result = cursor.fetchone()
        count = result[0] if result is not None else 0

        conn.close()
        return count

    except Exception as e:
        print("Error in count_equipped_skills:", e)
        conn.close()
        return 0  


    