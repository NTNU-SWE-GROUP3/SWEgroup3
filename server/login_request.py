from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/test', methods=['POST'])
def receive_login_request():
    
    account = request.form.get('Account') 
    password = request.form.get('Password')  

    # 打印存储的数据
    print("Received Account:", account)
    print("Received Password:", password)

    return jsonify({"message": "Data received successfully"})

if __name__ == '__main__':
    app.run(host='127.0.0.1', port=5000)  
