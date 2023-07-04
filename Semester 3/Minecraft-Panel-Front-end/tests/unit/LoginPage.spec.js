import {shallowMount, createLocalVue, mount} from '@vue/test-utils'
import Login from '@/components/LoginComponent'
import Vuex from 'vuex'

describe('Login', () => {
    it('checks if input fields represents props', async () => {
        const localVue = createLocalVue();
        localVue.use(Vuex)

        const store = new Vuex.Store({
            state: { auth: { status: { loggedIn: false }, user: null }}
        })

        const email = 'test@test.nl';
        const password = 'Password';


        const wrapper = mount(Login, {
            store,
            localVue,
            propsData: {
                form: {
                    email: email,
                    password: password
                }
            },
            stubs: ['b-form-group', 'b-form-input', 'b-form', 'b-button']
        });

        expect(wrapper.find('#input-1').text()).toEqual(email);
        expect(wrapper.find('#input-2').text()).toEqual(password);
    })
})