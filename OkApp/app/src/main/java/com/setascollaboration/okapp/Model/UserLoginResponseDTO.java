package com.setascollaboration.okapp.Model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@JsonIgnoreProperties(ignoreUnknown = true)
public class UserLoginResponseDTO {


        private UserLoginResultDTO result;

        public UserLoginResultDTO getResult() {
            return result;
        }

        public void setResult(UserLoginResultDTO result) {
            this.result = result;
        }



}
