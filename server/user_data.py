from flask import current_app
from flask import Blueprint
from flask import jsonify
from flask import request
from db import DBManager

import secrets, string, datetime


user_data = Blueprint('user_data', __name__, url_prefix='/user_data')

conn = None


''' ==============================
CardInfo Request
input
    
output
    result_text
        
============================== '''
@user_data.route('/getcardinfo', methods=['POST'])
def GetCardInfo():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Success
    data = conn.GetCardInfo()
    text_data = [';'.join(map(str, item)) for item in data]
    result_text = ';'.join(text_data).replace(' ', '')
    return result_text
                   


''' ==============================
SkillInfo Request
input
    
output
    result_text
        
============================== '''
@user_data.route('/getskillinfo', methods=['POST'])
def GetSkillInfo():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')

    # Success
    data = conn.GetSkillInfo()
    text_data = [';'.join(map(str, item)) for item in data]
    result_text = ';'.join(text_data).replace(' ', '')
    return result_text


''' ==============================
userskilldata Request
input
    Token
output
    result_text
        
============================== '''
@user_data.route('/getuserskilldata', methods=['POST'])
def GetUserSkillData():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')
        
    
    # Input
    token = request.form.get('Token')

    current_app.logger.info("token: %s", token)
    
    # Success
    data = conn.GetSkillData(token)
    text_data = [';'.join(map(str, item)) for item in data]
    result_text = ';'.join(text_data).replace(' ', '')
    
    #current_app.logger.info(data)
    
    return result_text


''' ==============================
usercarddata Request
input
    Token
output
    result_text
        
============================== '''
@user_data.route('/getuserstyledata', methods=['POST'])
def GetUserStyleData():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')
        
    
    # Input
    token = request.form.get('Token')

    current_app.logger.info("token: %s", token)
    
    # Success
    data = conn.GetStyleData(token)
    text_data = [';'.join(map(str, item)) for item in data]
    result_text = ';'.join(text_data).replace(' ', '')
    
    #current_app.logger.info(data)
    
    return result_text





''' ==============================
user_accountdata_table Request
input
    Token
output
    result_text
        
============================== '''
@user_data.route('/get_accountdata_table', methods=['POST'])
def GetAccountDataTable():

    # DataBase connection
    global conn
    if not conn:
        conn = DBManager(password_file='/run/secrets/db-password')
        
    
    # Input
    token = request.form.get('Token')

    current_app.logger.info("token: %s", token)
    
    # Success
    data = conn.GetAccountDataTableAll(token)
    text_data = [';'.join(map(str, item)) for item in data]
    result_text = ';'.join(text_data).replace(' ', '')
    
    #current_app.logger.info(data)
    
    return result_text