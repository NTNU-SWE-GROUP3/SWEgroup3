from flask import current_app
from flask import Flask
from flask import jsonify
from flask import request
from backpack import BPManager
from flask import Blueprint
import secrets, string, datetime

card_style = Blueprint('card_style', __name__, url_prefix='/card_style')
conn = None


''' =========================
equip skin
input
    token ID
    target card style
output
    status
        200001 : equip success
        200021 : user doesn't have this skin in inventory
        403011 : Token Expired
========================= '''
@card_style.route("/equip_card_style", methods=['POST'])
def EquipCardStyle():
    
    #database connection
    global conn
    if not conn:
        conn = BPManager(password_file='/run/secrets/db-password')

    #input
    token = request.form.get('Token')
    # targetCardStyleID = request.form.get('card_style_id')
    current_app.logger.info("tokenID: %s", token)
    # current_app.logger.info("target card style id: %s", targetCardStyleID)
    
    #check token validity
    expiredtime = conn.GetTokenExpiredTime(token)
    current_app.logger.info("Expired time: %s", expiredtime)
    now = datetime.datetime.now()
    current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
    if (expiredtime == -1):
        return jsonify(status = "403011", msg = "No such token")
    elif(now > expiredtime):
        return jsonify(status = "403011", msg = "Token expired")
    
    targetCardStyle = request.form.get('TargetCardStyle')
    current_app.logger.info("target card style: %s", targetCardStyle)

    #check if user have the card style
    cardStyle = conn.GetCardStyle(token, targetCardStyle)
    current_app.logger.info("have card style: ", cardStyle)
    if cardStyle==-1:
        current_app.logger.info("User does not have this item")
        return jsonify(status = "200021")
    
    # #success
    conn.EquipCardStyle(token, targetCardStyle)
    current_app.logger.info("User has successfully equipped this item")
    return jsonify(status="200001")

''' =========================
display skin
input
    token ID
    target card style
output
    status
        400004 : success
        403011 : Token Expired
========================= '''

'''@card_style.route("/display_card_style", methods=['POST'])
def DisplayCardStyle():
    
    #database connection
    global conn
    if not conn:
        conn = BPManager(password_file='/run/secrets/db-password')

    #input
    token = request.form.get('Token')
    # targetCardStyleID = request.form.get('card_style_id')
    current_app.logger.info("tokenID: %s", token)
    # current_app.logger.info("target card style id: %s", targetCardStyleID)
    # current_app.logger.info(request.form.items())
    
    #check token validity
    expiredtime = conn.GetTokenExpiredTime(token)
    current_app.logger.info("Expired time: %s", expiredtime)
    now = datetime.datetime.now()
    current_app.logger.info("Now: %s || Expired time: %s)", now, expiredtime)
    if (expiredtime == -1):
        return jsonify(status = "403011", msg = "No such token")
    elif(now > expiredtime):
        return jsonify(status = "403011", msg = "Token expired")
    
    targetCardStyle = request.form.getlist('TargetCardStyle')
    current_app.logger.info("target card style: %s", targetCardStyle)

    #success
    cardStyle = conn.DisplayCardStyle(token, targetCardStyle)

    #return card style list
    return jsonify(status = "400004",
                   msg = "Success",
                   cardStyle = cardStyle)'''

''' =========================
sell skin
input
    token ID
    target card style
output
    status
        200002 : sell success
        200022 : user doesn't have this skin in inventory
========================= 
@card_style.route('/sell_card_style', methods=['POST'])
def SellCardStyle():
    global conn
    if not conn:
        conn = BPManager(password_file='/run/secrets/db-password')

    #input
    accountID = request.form.get('id')
    targetCardStyleID = request.form.get('card_style_id')

    current_app.logger.info("accountID: %s", accountID)
    current_app.logger.info("targetCardStyleID: %s", targetCardStyleID)

    #check if user have the card style
    if not(conn.HaveCardStyle(accountID, targetCardStyleID)):
        current_app.logger.info("User does not have this item")
        return jsonify(status = "200022")
    
    #success
    conn.SellCardStyle(accountID, targetCardStyleID)
    return jsonify(status='200002')'''