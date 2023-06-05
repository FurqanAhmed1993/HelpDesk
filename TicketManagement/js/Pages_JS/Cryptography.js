function SubmitsEncry(PlainText) {

    var key = CryptoJS.enc.Utf8.parse('8080808080808080');
    var iv = CryptoJS.enc.Utf8.parse('8080808080808080');

    var encryptedText = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(PlainText), key,
        {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });

    console.log("encryptedText");
    return encryptedText;
}