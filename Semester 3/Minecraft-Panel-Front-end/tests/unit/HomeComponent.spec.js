import {createLocalVue, mount} from "@vue/test-utils";
import Home from "@/views/Home";
import Vuex from "vuex";

describe('ServerComponent.vue', () => {
    it('render "login" on home component', () => {
        const localVue = createLocalVue();

        localVue.use(Vuex)

        const store = new Vuex.Store({
            state: { auth: { status: { loggedIn: false }, user: null }}
        })

        const wrapper = mount(Home, {
            store,
            localVue,
            stubs: ['router-link']
        });

        expect(wrapper.find('.loginRedirect').text()).toBe('login');
    })

    it('render "server page" on home component', () => {
        const localVue = createLocalVue();

        localVue.use(Vuex)

        const store = new Vuex.Store({
            state: { auth: { status: { loggedIn: true }, user: "null" }}
        })

        const wrapper = mount(Home, {
            store,
            localVue,
            stubs: ['router-link']
        });

        expect(wrapper.find('.loginRedirect').text()).toBe('server page');
    })
})