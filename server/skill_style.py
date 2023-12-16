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

''' ==============================
getplayerdata Request (get player skill)
input
    token ID
    target skill
output
    status
        200040 : equip Success
        200041 : equip failed
        200052 : user doesnt have this item
============================== '''

# @skill_style.route('/equip_skill_style', methods=['POST'])

# def EquipSkillStyle():

#     # DataBase connection
#     # global conn
#     # if not conn:
#     #     conn = BPManager(password_file='/run/secrets/bp-password')

#     if not connection:
#         print("Failed to connect to the database.")
#         return False
    
#     # Input
#     token = request.form.get('Token')
#     #skill_id = request.form.get('SkillId')
#     current_app.logger.info("token: %s", token)

#     # # Input validation
#     # # there should check if the input are dangerous

#     #token expiredtime
#     expiredtime = conn.GetTokenExpiredTime(token)
#     current_app.logger.info("Expired time: %s", expiredtime)
#     now = datetime.datetime.now()
#     current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
#     if (expiredtime == -1):
#         return jsonify(status = "403011", msg = "No such token")
#     elif(now > expiredtime):
#         return jsonify(status = "403011", msg = "Token expired")
    
#     targetSkillStyle = request.form.get('TargetSkillStyle')
#     current_app.logger.info("target skill style: %s", targetSkillStyle)

#     #check if user have the skill style
#     skillStyle = conn.GetSkillStyle(token, targetSkillStyle)
#     current_app.logger.info("have skill style: ", skillStyle)
#     if skillStyle==-1:
#         current_app.logger.info("User does not have this item")
#         return jsonify(status = "200051", token = token)
    
#     #success
#     conn.EquipSkillStyle(token, targetSkillStyle)
#     current_app.logger.info("User has successfully equipped this item")
#     return jsonify(status="200040")

# # DISPLAY SKILL IN UNITY ?

# @skill_style.route("/display_skill_style", methods=['POST'])
# def DisplaySkillStyle():
    
#     # #database connection
#     # global conn
#     # if not conn:
#     #     conn = BPManager(password_file='/run/secrets/db-password')

#     connection = func.create_mysql_connection()

#     if not connection:
#         print("Failed to connect to the database.")
#         return False
    
#     cursor = connection.cursor()

#     #input
#     #skill_id = request.form.get('SkillId')
#     token = request.form.get('Token')
#     current_app.logger.info("tokenID: %s", token)
#     #current_app.logger.info("target skill id: %s", skill_id)
#     current_app.logger.info(request.form.items())
    
#     #check token validity
#     expiredtime = conn.GetTokenExpiredTime(token)
#     current_app.logger.info("Expired time: %s", expiredtime)
#     now = datetime.datetime.now()
#     current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
#     if (expiredtime == -1):
#         return jsonify(status = "403011", msg = "No such token")
#     elif(now > expiredtime):
#         return jsonify(status = "403011", msg = "Token expired")

#     #targetSkill = request.form.get('SkillId')
#     #current_app.logger.info("target skill: %d", targetSkill)

#     #success
#     skillStyle = conn.DisplaySkillStyle(token)
#     current_app.logger.info("Retrieved skillStyle: %s", skillStyle)

#     #return skill list
#     return jsonify(status = "400055",
#                    msg = "Success",
#                    token = token,
#                    skillStyles = skillStyle)


# @skill_style.route("/display_skill_desc", methods=['POST'])
# def DisplaySkillDesc():
    
#     # #database connection
#     # global conn
#     # if not conn:
#     #     conn = BPManager(password_file='/run/secrets/db-password')

#     connection = func.create_mysql_connection()

#     if not connection:
#         print("Failed to connect to the database.")
#         return False

#     #input
#     #skill_id = request.form.get('SkillId')
#     token = request.form.get('Token')
#     current_app.logger.info("tokenID: %s", token)
#     #current_app.logger.info("target skill id: %s", skill_id)
#     current_app.logger.info(request.form.items())
    
#     #check token validity
#     expiredtime = conn.GetTokenExpiredTime(token)
#     current_app.logger.info("Expired time: %s", expiredtime)
#     now = datetime.datetime.now()
#     current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
#     if (expiredtime == -1):
#         return jsonify(status = "403011", msg = "No such token")
#     elif(now > expiredtime):
#         return jsonify(status = "403011", msg = "Token expired")

#     #targetSkill = request.form.get('SkillId')
#     #current_app.logger.info("target skill: %d", targetSkill)

#     #success
#     skillName = conn.getSkillName(token)
#     skillDesc = conn.getSkillDesc(token)
#     skillProb = conn.getSkillProb(token)
#     current_app.logger.info("Retrieved skillname: %s", skillName)
#     current_app.logger.info("Retrieved skilldesc: %s", skillDesc)
#     current_app.logger.info("Retrieved skillprob: %s", skillProb)

#     #return skill list
#     return jsonify(status = "400055",
#                    msg = "Success",
#                    token = token,
#                    skillName = skillName,
#                    skillDesc = skillDesc,
#                    skillProb = skillProb)

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

@skill_style.route("/display_skill_desc", methods=['POST'])
def DisplaySkillDesc():
    try:
        skill_id = request.form.get('Skill_id')
        #token = request.form.get('Token')
        #current_app.logger.info("tokenID: %s", token)

        skillDesc = GetSkillDesc(skill_id)
        return skillDesc

    except Exception as e:
        print("Error in displayskillstyle:", e)
        return None
    
def GetSkillDesc(skill_id):
    try:
        connection = func.create_mysql_connection()
        if not connection:
            print("Failed to connect to the database.")
            return None

        cursor = connection.cursor()
        cursor.execute("SELECT skill_name, skill_description, skill_probability FROM skill WHERE skill_id = %s", (skill_id))
        response = cursor.fetchall()
        print("Skilldesc = ", response)
        connection.close()
        return response
    
    except Exception as e:
        print("Error in Getskilldesc:", e)
        return None
    
@skill_style.route("/equip_skill", methods=['POST'])
def EquipSkill():
    try:
        tokenId = request.form.get("tokenId")
        targetSkillId = request.form.get("targetSkillId")

        print("Token ID: ", tokenId)
        print("Target skill ID: ", targetSkillId)

        haveItem = HaveSkillstyle(tokenId, targetSkillId)
        print("haveItem: ", haveItem)

        if haveItem:
            unselectSkinId = FindEquippedskillstyle(tokenId)
            print("target unselect skin id: ", unselectSkinId)
            if unselectSkinId:
                unEquipSuccess = UnEquipSkillstyle(tokenId, unselectSkinId)
                if not unEquipSuccess:
                    print("Failed to unequip skin")
                    return False
                print("Succeeded to unequip skin")
            equipSuccess = UpdateEquipStatus(tokenId, targetSkillId)
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
    
def HaveSkillstyle(tokenId, targetSkillId):
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
            (tokenId, targetSkillId,))
        
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
        print("Error in HaveSkillstyle:", e)
        conn.close()
        return False
    
def UpdateEquipStatus(tokenId, targetSkillId):
    try:
        conn = func.create_mysql_connection()
        cursor = conn.cursor()
        cursor.execute(
            "UPDATE account_card_style acs "
            "JOIN account a ON a.id = acs.account_id "
            "SET acs.equip_status = 1 "
            "WHERE a.token_id = %s AND acs.card_style_id = %s",
            (tokenId, targetSkillId,)
        )
        conn.commit()
        conn.close()
        print("Successfully equipped skill")
        return True     

    except Exception as e:
        print("Error in Equipskillstyle")
        conn.close()
        return False
    
def UnEquipskillstyle(tokenId, targetSkillId):
    try:
        conn = func.create_mysql_connection()
        cursor = conn.cursor()
        cursor.execute(
            "UPDATE account_card_style acs "
            "JOIN account a ON a.id = acs.account_id "
            "SET acs.equip_status = 0 "
            "WHERE a.token_id = %s AND acs.card_style_id = %s",
            (tokenId, targetSkillId,)
        )
        conn.commit()
        conn.close()
        print("Successfully unequipped skill")
        return True
    except Exception as e:
        print("Error in UnEquipskillstyle")
        conn.close()
        return False

def FindEquippedskillstyle(tokenId):
    try:
        conn = func.create_mysql_connection()
        cursor = conn.cursor()
        cursor.execute(
            "SELECT acs.card_style_id "
            "FROM account a "
            "JOIN account_card_style acs ON a.id = acs.account_id "
            "WHERE a.token_id = %s AND acs.equip_status = 1",
            (tokenId,)
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
        print("Error in FindEquippedskillstyle")
        conn.close()
        return None
    