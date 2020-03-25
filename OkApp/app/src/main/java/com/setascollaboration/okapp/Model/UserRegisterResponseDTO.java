package com.setascollaboration.okapp.Model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class UserRegisterResponseDTO {

    private UserRegisterResultDTO result;

    public UserRegisterResultDTO getResult() {
        return result;
    }

    public void setResult(UserRegisterResultDTO result) {
        this.result = result;
    }

}
