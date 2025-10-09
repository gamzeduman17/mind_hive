import React, { useState } from "react";
import { Alert, Image, StyleSheet, TouchableOpacity, View } from "react-native";
import { Text, TextInput, Button, Checkbox } from "react-native-paper";
import { login } from "../../api/services/authService";
import { LoginRequestModel } from "../../models/LoginModels";

export default function LoginScreen({ navigation }: any) {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [errors, setErrors] = useState<{ userName?: string, password?: string }>({});

  const handleLogin = async () => {
    let newErrors: any = {};
    if (!userName.trim()) {
      newErrors.userName = "Username is required";
    }
    if (!password.trim()) {
      newErrors.userName = "Password is required";
    }
    setErrors(newErrors);
    if (Object.keys(newErrors).length > 0) {
      return;
    }
    try {
      let req: LoginRequestModel = {
        Username: userName,
        Password: password
      }
      const result = await login(req);
      Alert.alert("Success", `Welcome ${result.Username} (${result.Role})`);
      navigation.navigate("Home");
    } catch (err: any) {
      Alert.alert("Error", err.message || "Login failed");
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
        autoCapitalize="none"
        autoCorrect={false}
        value={userName}
        onChangeText={setUserName}
        style={styles.input}
      />
      {errors.userName && <Text style={styles.errorText}>{errors.userName}</Text>}

      {/* Password */}
      <View style={{ width: "100%", position: "relative" }}>
        <TextInput
          label="Password"
          mode="outlined"
          secureTextEntry={!showPassword}
          value={password}
          autoCapitalize="none"
          autoCorrect={false}
          onChangeText={setPassword}
          style={styles.input}
        />
        <TouchableOpacity
          style={[styles.eyeIcon, { position: "absolute", right: 10, top: 15 }]}
          onPress={() => setShowPassword(!showPassword)}
        >
          <Text>{showPassword ? "Hide" : "Show"}</Text>
          {/* change with eye later */}
        </TouchableOpacity>
      </View>
      {errors.password && <Text style={styles.errorText}>{errors.password}</Text>}


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
  errorText: {
    color: 'red',
    fontSize: 12,
    marginBottom: 10,
  },
  eyeIcon: {
    padding: 5,
  },
});
