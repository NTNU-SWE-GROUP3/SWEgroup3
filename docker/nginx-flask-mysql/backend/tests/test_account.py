import secrets
import string
from app import server


def generateRandomString():
    alphabet = string.ascii_letters + string.digits
    return ''.join(secrets.choice(alphabet) for i in range(5))


def test_AccountLogin_NoSuchAccount():
    # Set a test client of flask application
    client = server.test_client()

    response = client.post("/account/login", data={
        "Account": "NoSuchAccount",
        "Password": "NoSuchAccount", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403001"
    assert response.json["tokenId"] == ""


def test_AccountLogin_WrongPassword():
    # Set a test client of flask application
    client = server.test_client()

    response = client.post("/account/login", data={
        "Account": "test0001",
        "Password": "WrongPassword", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403002"
    assert response.json["tokenId"] == ""


def test_AccountLogin_Success():
    # Set a test client of flask application
    client = server.test_client()

    response = client.post("/account/login", data={
        "Account": "test0001",
        "Password": "test0001pass", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "400000"
    assert response.json["tokenId"] != ""


# status
#     400001 : Success
#     403003 : Username has been used
#     403004 : Email already registered
#     403005 : Password too short
# token ID
def test_AccountSignUp_UsernameHasBeenUsed():
    # Set a test client of flask application
    client = server.test_client()

    response = client.post("/account/signup", data={
        "Account": "test0001",
        "Password": "IamNotTheFirst",
        "Email": "IamNotTheFirst@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403003"
    assert response.json["tokenId"] == ""


def test_AccountSignUp_EmailAlreadyRegistered():
    # Set a test client of flask application
    client = server.test_client()

    randStr = generateRandomString()

    response = client.post("/account/signup", data={
        "Account": "IamTheFirst" + randStr,
        "Password": "EmailAlreadyRegistered",
        "Email": "test0006@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403004"
    assert response.json["tokenId"] == ""


def test_AccountSignUp_PasswordTooShort():
    # Set a test client of flask application
    client = server.test_client()

    randStr = generateRandomString()

    response = client.post("/account/signup", data={
        "Account": "IamTheFirst" + randStr,
        "Password": "123456789",
        "Email": "PasswordTooShort" + randStr + "@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "403005"
    assert response.json["tokenId"] == ""


def test_AccountSignUp_Success():
    # Set a test client of flask application
    client = server.test_client()

    randStr = generateRandomString()

    response = client.post("/account/signup", data={
        "Account": "IamTheFirst" + randStr,
        "Password": "1234567890",
        "Email": "signup.success." + randStr + "@ntnu.edu.tw", })

    # Validation
    assert response.status_code == 200
    assert response.json["status"] == "400001"
    assert response.json["tokenId"] != ""
