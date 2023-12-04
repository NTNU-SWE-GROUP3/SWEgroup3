from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/test', methods=['POST'])
def receive_post_request():
    data = request.form.get('key')  # 获取来自Unity的数据

    # 在这里处理接收到的数据
    # 你可以执行任何你需要的操作

    return jsonify({"message": "Data received successfully"})

if __name__ == '__main__':
    app.run(host='127.0.0.1', port=5000)  # 启动Flask服务器
