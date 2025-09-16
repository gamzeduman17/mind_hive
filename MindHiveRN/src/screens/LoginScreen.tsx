import React, { useState } from "react";
import { Image, StyleSheet, View } from "react-native";
import { Text, TextInput, Button, Checkbox } from "react-native-paper";

export default function LoginScreen({ navigation }: any) {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);

  const handleLogin = () => {
    if (userName && password) {
      navigation.navigate("Home");
    } else {
      // Paper'ın Snackbar ya da AlertDialog ile daha modern hata mesajı da verebiliriz.
      console.log("Please enter username and password");
    }
  };

  return (
    <View style={styles.container}>
      {/* App Logo */}
      <Image
        source={require("../assets/images/app.jpg")}
        style={styles.appLogo}
        resizeMode="contain"
      />

      {/* Title */}
      <Text style={styles.title}>Welcome to MindHive</Text>

      {/* Username */}
      <TextInput
        label="Username"
        mode="outlined"
        value={userName}
        onChangeText={setUserName}
        style={styles.input}
      />

      {/* Password */}
      <TextInput
        label="Password"
        mode="outlined"
        secureTextEntry
        value={password}
        onChangeText={setPassword}
        style={styles.input}
      />

      {/* Remember me + Forgot password */}
      <View style={styles.optionsRow}>
        <View style={styles.rememberRow}>
          <Checkbox
            status={rememberMe ? "checked" : "unchecked"}
            onPress={() => setRememberMe(!rememberMe)}
          />
          <Text>Remember me</Text>
        </View>

        <Text
          style={styles.forgotText}
          onPress={() => console.log("Forgot password pressed")}
        >
          Forgot Password?
        </Text>
      </View>

      {/* Login button */}
      <Button
        mode="contained"
        onPress={handleLogin}
        style={styles.loginButton}
      >
        Login
      </Button>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 24,
    justifyContent: "center",
    backgroundColor: "#fff",
  },
  appLogo: {
    width: 120,
    height: 120,
    alignSelf: "center",
    marginBottom: 20,
  },
  title: {
    textAlign: "center",
    fontSize: 22,
    marginBottom: 30,
  },
  input: {
    marginBottom: 15,
  },
  optionsRow: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 20,
  },
  rememberRow: {
    flexDirection: "row",
    alignItems: "center",
  },
  forgotText: {
    color: "#1976d2",
    textDecorationLine: "underline",
  },
  loginButton: {
    marginTop: 10,
    paddingVertical: 5,
  },
});
