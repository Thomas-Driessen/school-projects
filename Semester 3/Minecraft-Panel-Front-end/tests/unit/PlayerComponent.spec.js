import {createLocalVue, mount, shallowMount} from '@vue/test-utils'
import PlayerComponent from '@/components/PlayerComponent.vue'

test('PlayerComponent', () => {
    const wrapper = mount(PlayerComponent, {
            stubs: ['b-table', "b-button"]
    });

    wrapper.setData({
        players: [
            {
                "id": "000",
                "username": "TestPlayer"
            }
        ]
    })

    expect(wrapper.find('.usersTable').text()).toBe('login');
})