from flask import Flask, request, jsonify
import random
import string
import hashlib
import secrets



def generate_random_salt():
    # 随机生成字符串长度，范围在 15 到 25 之间
    string_length = random.randint(15, 25)
    
    # 从字母和数字的组合中随机选择字符生成字符串
    random_string = ''.join(random.choice(string.ascii_letters + string.digits) for _ in range(string_length))
    
    return random_string


app = Flask(__name__)

@app.route('/login', methods=['POST'])
def receive_login_request():
    '''
    status
    400000 -- Success
    403001 -- No such account
    403002 -- Wrong password
    
    ''' 
    account = request.form.get('Account') 
    password = request.form.get('Password')  
    
    # there should check if the input are dangerous, but we can skip that temporary
    
    if(1):  # Search for the database, find wheather account exist
        pass
    else:
        return jsonify({"status":"403001"})
    
    salt = "0Gih6rJGfCXE72QTrX" # generate_random_salt() #This should derive from database, however, there is not such thing in it
    
    # 连接密码和盐值
    password_with_salt = password + salt

    #  SHA-256 for hash calculation
    hashed_password = hashlib.sha256(password_with_salt.encode()).hexdigest()

    user_password = "d48d4f28fbb43edb0138ad46fb64a5d3bcda8d05f6e087743213ca3b21908892"  #This should derive from database, however, dont't know how to do it now LOL
    
    if(hashed_password != user_password):
        return jsonify({"status":"403002"})
    
    #there shoud have the process to check wheather the token itself is valid

    if(0):  #the token still valid
        pass
    else:
        token = secrets.token_hex(16)
        # The token should be update to the database
       
    return jsonify({"status":"400000","token":token})


@app.route('/signup', methods=['POST'])
def receive_signup_request():
    
    '''
    status
    400001 -- Success
    403003 -- Username has been used
    403004 -- Email already registered
    403005 -- Password too short
    
    '''    
    return jsonify({"status":"400001"})

    return jsonify({"status":"403003"})

    return jsonify({"status":"403004"})

    return jsonify({"status":"403005"})


@app.route('/checkaccount', methods=['POST'])
def receive_checkaccount_request():
    
    '''
    status
    400002 -- Success
    403001 -- No such account
    403006 -- Email & account NOT match
    
    '''    
    return jsonify({"status":"400002"})
    return jsonify({"status":"403001"})
    return jsonify({"status":"403006"})


@app.route('/changepassword', methods=['POST'])
def receive_changepassword_request():
    
    '''
    status
    400003 -- Success
    403007 -- Password too short
    403008 -- Password too long
    
    '''    
    return jsonify({"status":"400003"})
    return jsonify({"status":"403007"})
    return jsonify({"status":"403008"})



if __name__ == '__main__':
    app.run(host='127.0.0.1', port=5000)  
