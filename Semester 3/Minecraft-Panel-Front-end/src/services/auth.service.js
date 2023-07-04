import axios from 'axios';
import APIConnection from '@/globals/APIConnectionURL'
import authHeader from "@/services/auth-header";

class AuthService {
    login(user) {
        return axios
            .post(APIConnection.APIAuthenticationURL + '/identity/login', {
                email: user.email,
                password: user.password
            })
            .then(response => {
                if (response.data.Token) {
                    localStorage.setItem('user', JSON.stringify(response.data));
                    axios.defaults.headers.common['Authorization'] = authHeader();
                }

                return response.data;
            });
    }

    logout() {
        localStorage.removeItem('user');
        axios.defaults.headers.common['Authorization'] = null;
    }

    register(user) {
        return axios.post(APIConnection.APIAuthenticationURL + '/api/main/user/registerAccount', {
            userDetails: {
                userName: user.username,
                email: user.email,
            },
            password: user.password
        });
    }
}

export default new AuthService();
