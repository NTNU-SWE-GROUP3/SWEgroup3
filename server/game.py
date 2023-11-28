import mysql.connector
from flask import Flask, request, jsonify, Blueprint

gaming = Blueprint('gaming',__name__, url_prefix='/gaming')

db_config = {
    "host": "localhost",
    "user": "swegroup3",
    "password": "Swegroup3@12345",
    "database": "game",
}

def create_mysql_connection():
    return mysql.connector.connect(**db_config,autocommit=True)

@gaming.route('/get_skills',method=['POST'])
def GetSkills():

