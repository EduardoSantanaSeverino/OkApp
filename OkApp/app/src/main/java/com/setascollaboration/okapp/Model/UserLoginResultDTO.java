package com.setascollaboration.okapp.Model;

public class UserLoginResultDTO {

    private String accessToken;
    private String encryptedAccessToken;
    private int expireInSeconds;
    private int userId;
    private int instaAccountId;

    public String getAccessToken() {
        return accessToken;
    }

    public void setAccessToken(String accessToken) {
        this.accessToken = accessToken;
    }

    public String getEncryptedAccessToken() {
        return encryptedAccessToken;
    }

    public void setEncryptedAccessToken(String encryptedAccessToken) {
        this.encryptedAccessToken = encryptedAccessToken;
    }

    public int getExpireInSeconds() {
        return expireInSeconds;
    }

    public void setExpireInSeconds(int expireInSeconds) {
        this.expireInSeconds = expireInSeconds;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
    }

    public int getInstaAccountId() {
        return instaAccountId;
    }

    public void setInstaAccountId(int instaAccountId) {
        this.instaAccountId = instaAccountId;
    }
}
