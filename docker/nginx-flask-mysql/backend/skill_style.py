# from flask import current_app
# from flask import Blueprint
# from flask import Flask
# from flask import jsonify
# from flask import request
# from db import DBManager
# from backpack import BPManager
# from flask import current_app
# import datetime

# skill_style = Blueprint('skill_style', __name__, url_prefix='/skill_style')

# conn = None

# ''' ==============================
# getplayerdata Request (get player skill)
# input
#     token ID
#     target skill
# output
#     status
#         200040 : equip Success
#         200051 : user doesnt have this item
#         403011 : Token Expired
# ============================== '''

# @skill_style.route('/equip_skill_style', methods=['POST'])

# def EquipSkillStyle():

#     # DataBase connection
#     global conn
#     if not conn:
#         conn = BPManager(password_file='/run/secrets/db-password')

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
    
#     #database connection
#     global conn
#     if not conn:
#         conn = BPManager(password_file='/run/secrets/db-password')

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
# <<<<<<< HEAD
#                    skillStyle = skillStyle)
# =======
#                    skillStyles = skillStyle)


# @skill_style.route("/display_skill_desc", methods=['POST'])
# def DisplaySkillDesc():
    
#     #database connection
#     global conn
#     if not conn:
#         conn = BPManager(password_file='/run/secrets/db-password')

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
# >>>>>>> 7c2b8cd9f01bdc5ebbed9c3352658f58b2d14f62

