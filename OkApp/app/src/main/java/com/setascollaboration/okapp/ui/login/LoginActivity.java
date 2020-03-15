package com.setascollaboration.okapp.ui.login;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.setascollaboration.okapp.MainActivity;
import com.setascollaboration.okapp.Model.User;
import com.setascollaboration.okapp.Model.UserDTO;
import com.setascollaboration.okapp.R;

import org.springframework.http.converter.json.MappingJackson2HttpMessageConverter;
import org.springframework.web.client.RestTemplate;

public class LoginActivity extends AppCompatActivity {
    private EditText passwordField;
    private EditText usernameField;
    private Spinner languageSpinner;
    private String ServerUrl;
    private TextView createAccount;
    private SharedPreferences myPreference;
    long delay = 3000; // 3 seconds after user stops typing
    long last_text_edit = 0;
    private AlertDialog.Builder builder;
    private AlertDialog dialog;
    Handler handler = new Handler();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        final Runnable input_finish_checker = new Runnable() {
            public void run() {
                if (System.currentTimeMillis() > (last_text_edit + delay - 500)) {
                    User c = new User(usernameField.getText().toString(), passwordField.getText().toString());
                    User[] userArray = {c};
                    dialog.show();
                    try {
                        new LoginRestTask().execute(userArray);
                    } catch (Exception e) {
                        Log.e("LoginActivity", e.getMessage());
                        Toast.makeText(getApplicationContext(), getResources().getString(R.string.server_error), Toast.LENGTH_LONG).show();
                    }
                }
            }
        };

        //Assign to specific elements
        usernameField = (EditText) findViewById(R.id.username);
        passwordField = (EditText) findViewById(R.id.password);
        createAccount = (TextView) findViewById(R.id.txtCreateAcc);

        createAccount.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                startActivity(intent);
            }
        });

        myPreference = getSharedPreferences("MyPrefs",MODE_PRIVATE);
        ServerUrl = myPreference.getString("IPAddress", "");
        builder = new AlertDialog.Builder(LoginActivity.this);
        builder.setCancelable(false); // if you want user to wait for some process to finish,
        builder.setView(R.layout.myprogress_dialog);
        dialog = builder.create();
        usernameField.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                passwordField.setText("");
            }

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });
        passwordField.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                handler.removeCallbacks(input_finish_checker);
            }

            @Override
            public void afterTextChanged(Editable editable) {
                if (editable.length() > 0) {
                    last_text_edit = System.currentTimeMillis();
                    handler.postDelayed(input_finish_checker, delay);
                } else {

                }
            }
        });

        languageSpinner = (Spinner) findViewById(R.id.spinner_language);
        //Populate the language dropdown
        Spinner spinner = (Spinner) findViewById(R.id.spinner_language);
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this, R.array.spinner_language, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);

    }

    private class LoginRestTask extends AsyncTask<User, Void, UserDTO> {

        @Override
        protected UserDTO doInBackground(User... users) {
            try {
                RestTemplate restTemplate = new RestTemplate();
                restTemplate.getMessageConverters().add(new MappingJackson2HttpMessageConverter());
                return restTemplate.postForObject(ServerUrl + "/api/users/login", users[0], UserDTO.class);
            } catch (Exception ex) {
                Log.e("ERROR: ", ex.getMessage());
                return null;
            }
        }

        @Override
        protected void onPostExecute(UserDTO userDTO) {
            super.onPostExecute(userDTO);

            dialog.dismiss();
            if(userDTO != null)
            {
                SharedPreferences.Editor prefEditor = myPreference.edit();
                prefEditor.putString("Token", userDTO.getToken());
                prefEditor.commit();
                Toast.makeText(getApplicationContext(),
                        getResources().getString(R.string.welcome)+", "+usernameField.getText().toString()+"!",
                        Toast.LENGTH_LONG).show();

                Intent intent = new Intent(getApplicationContext(), MainActivity.class);
                intent.putExtra("statuscode", 1);
                startActivity(intent);
            }
            else
            {
                AlertDialog alertDialog = new AlertDialog.Builder(LoginActivity.this).create();
                alertDialog.setCancelable(false);
                alertDialog.setTitle(getResources().getString(R.string.alert));
                alertDialog.setMessage(getResources().getString(R.string.login_error));

                alertDialog.setButton(AlertDialog.BUTTON_NEUTRAL, "OK",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int which) {

                            }
                        });
                alertDialog.show();
            }

        }
    }
}
