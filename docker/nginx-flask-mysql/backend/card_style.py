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
========================= '''
@card_style.route("/equip_card_style", methods=['POST'])
def EquipCardStyle():
    
    #database connection
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
        return jsonify(status = "200021")
    
    #success
    conn.EquipCardStyle(accountID, targetCardStyleID)
    return jsonify(status="200001")

''' =========================
sell skin
input
    token ID
    target card style
output
    status
        200002 : sell success
        200022 : user doesn't have this skin in inventory
========================= '''
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
    return jsonify(status='200002')