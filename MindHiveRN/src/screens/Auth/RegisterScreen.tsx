import React, { useState } from 'react';
import { View, TextInput, Button, Text, StyleSheet, Alert } from 'react-native';
import apiClient from '../../api/apiClient';


export default function RegisterScreen({ navigation }: any) {
    const [form, setForm] = useState({
        username: '',
        password: '',
        confirmPassword: '',
        email: '',
        name: '',
        surname: ''
    });

    const [error, setError] = useState(null);

    const handleChange = (key: any, value: any) => {
        setForm({ ...form, [key]: value });
    };

    const handleRegister = async () => {
        try {
            const response = await apiClient.post('auth/register', form);
            const result = response.data;
            console.log('Register response:', result);

            if (result.success) {
                Alert.alert('Registration successful! You are being redirected to the login page.');
                navigation.navigate('Login');
            } else {
                setError(result.message || 'Kayıt başarısız');
            }
        } catch (err: any) {
            console.error('Register error:', err);
            Alert.alert("Error", err.message || "An error occurred while connecting to the server");
        }
    };

    return (
        <View style={styles.container}>
            <TextInput
                placeholder="Username"
                style={styles.input}
                value={form.username}
                onChangeText={(t) => handleChange('username', t)}
            />
            <TextInput
                placeholder="Email"
                style={styles.input}
                value={form.email}
                onChangeText={(t) => handleChange('email', t)}
            />
            <TextInput
                placeholder="Name"
                style={styles.input}
                value={form.name}
                onChangeText={(t) => handleChange('name', t)}
            />
            <TextInput
                placeholder="Surname"
                style={styles.input}
                value={form.surname}
                onChangeText={(t) => handleChange('surname', t)}
            />
            <TextInput
                placeholder="Password"
                style={styles.input}
                secureTextEntry
                value={form.password}
                onChangeText={(t) => handleChange('password', t)}
            />
            <TextInput
                placeholder="Confirm Password"
                style={styles.input}
                secureTextEntry
                value={form.confirmPassword}
                onChangeText={(t) => handleChange('confirmPassword', t)}
            />

            {error && <Text style={styles.error}>{error}</Text>}

            <Button title="Register" onPress={handleRegister} />
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        justifyContent: 'center'
    },
    input: {
        borderWidth: 1,
        borderColor: '#ccc',
        marginBottom: 10,
        padding: 10,
        borderRadius: 5
    },
    error: {
        color: 'red',
        marginBottom: 10,
        textAlign: 'center'
    }
});
