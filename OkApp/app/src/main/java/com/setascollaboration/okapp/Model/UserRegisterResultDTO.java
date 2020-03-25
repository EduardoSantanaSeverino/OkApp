package com.setascollaboration.okapp.Model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class UserRegisterResultDTO {

    private String canLogin;
    private String tenancyName;

    public String getCanLogin() {
        return canLogin;
    }

    public void setCanLogin(String canLogin) {
        this.canLogin = canLogin;
    }

    public String getTenancyName() {
        return tenancyName;
    }

    public void setTenancyName(String tenancyName) {
        this.tenancyName = tenancyName;
    }
}



