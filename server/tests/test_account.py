import secrets
import string
import sys, os
sys.path.append(os.path.abspath("server"))
from main import app


def generateRandomString():
    alphabet = string.ascii_letters + string.digits
    return ''.join(secrets.choice(alphabet) for i in range(10))


def generateTestUser():
    client = app.test_client()

    response = client.post("/account/signup", data={
        "Account": "test000001",
        "Password": "test000001pass",
        "Email": "test000001@ntnu.edu.tw", })

    assert response.status_code == 200


def test_AccountLogin_NoSuchAccount():
    # Set a test client of flask application
    client = app.test_client()

    response = client.post("/account/login", data={
        "Account": "NoSuchAccount",
        "Password": "NoSuchAccount", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403001"
    assert response.json["tokenId"] == ""


def test_AccountLogin_WrongPassword():
    # Prepare for testing
    generateTestUser()

    # Set a test client of flask application
    client = app.test_client()

    response = client.post("/account/login", data={
        "Account": "test000001",
        "Password": "WrongPassword", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403002"
    assert response.json["tokenId"] == ""


def test_AccountLogin_Success():
    # Prepare for testing
    generateTestUser()

    # Set a test client of flask application
    client = app.test_client()

    response = client.post("/account/login", data={
        "Account": "test000001",
        "Password": "test000001pass", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "400000"
    assert response.json["tokenId"] != ""


def test_AccountSignUp_UsernameHasBeenUsed():
    # Prepare for testing
    generateTestUser()

    # Set a test client of flask application
    client = app.test_client()

    response = client.post("/account/signup", data={
        "Account": "test000001",
        "Password": "UsernameHasBeenUsed",
        "Email": "UsernameHasBeenUsed@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403003"
    assert response.json["tokenId"] == ""


def test_AccountSignUp_EmailAlreadyRegistered():
    # Prepare for testing
    generateTestUser()

    # Set a test client of flask application
    client = app.test_client()

    randStr = generateRandomString()

    response = client.post("/account/signup", data={
        "Account": "EmailAlreadyRegistered",
        "Password": "EmailAlreadyRegistered",
        "Email": "test000001@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403004"
    assert response.json["tokenId"] == ""


def test_AccountSignUp_PasswordTooShort():
    # Set a test client of flask application
    client = app.test_client()

    randStr = generateRandomString()

    response = client.post("/account/signup", data={
        "Account": "PasswordTooShort",
        "Password": "123456789",
        "Email": "PasswordTooShort@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403005"
    assert response.json["tokenId"] == ""


def test_AccountSignUp_Success():
    # Set a test client of flask application
    client = app.test_client()

    randStr = generateRandomString()

    response = client.post("/account/signup", data={
        "Account": "test000001" + randStr,
        "Password": "1234567890",
        "Email": "test000001" + randStr + "@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "400001"
    assert response.json["tokenId"] != ""


def test_AccountLogin_SqlInjection():
    # Set a test client of flask application
    client = app.test_client()

    # Case normal
    response = client.post("/account/sql/injection/test", data={
        "Account": "test000001", })
    assert response.status_code == 200
    assert response.json["result"] != [], f"result should not be empty."

    # Case 1
    response = client.post("/account/sql/injection/test", data={
        "Account": "test000001'; select * from account; -- ", })
    assert response.status_code == 200
    assert response.json["result"] == [], f"result should be empty."

    # Case 2
    response = client.post("/account/sql/injection/test", data={
        "Account": "' or 1=1; -- ", })
    assert response.status_code == 200
    assert response.json["result"] == [], f"result should be empty."
