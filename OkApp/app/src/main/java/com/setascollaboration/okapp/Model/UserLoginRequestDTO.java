package com.setascollaboration.okapp.Model;

public class UserLoginRequestDTO {
    public String getUserNameOrEmailAddress() {
        return userNameOrEmailAddress;
    }

    public UserLoginRequestDTO(String userNameOrEmailAddress, String password, boolean rememberClient) {
        this.userNameOrEmailAddress = userNameOrEmailAddress;
        this.password = password;
        this.rememberClient = rememberClient;
    }

    public void setUserNameOrEmailAddress(String userNameOrEmailAddress) {
        this.userNameOrEmailAddress = userNameOrEmailAddress;
    }

    public boolean isRememberClient() {
        return rememberClient;
    }

    public void setRememberClient(boolean rememberClient) {
        this.rememberClient = rememberClient;
    }

    private String userNameOrEmailAddress;
    private String password;
    private boolean rememberClient;

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

}
