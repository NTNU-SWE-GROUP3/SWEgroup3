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