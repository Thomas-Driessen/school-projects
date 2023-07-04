
describe('ServerComponent.vue', () => {
    it('renders name when method called', () => {
        cy.visit("http://localhost:8080/#/")

        cy.get('.toggleSideBarMenu').click();

        cy.contains('Login').click();

        cy.get('#input-1').type(Cypress.env('testUserEmail'));
        cy.get('#input-2').type(Cypress.env('testPassword'));

        cy.get('.loginForm').then(form$ => {
            form$.on("submit", e => {
                e.preventDefault();
            });
        });

        cy.get('.loginForm').submit();
        cy.wait(1500);
        cy.get('a[href*="#/server"]').click();

        cy.get('.getPluginsButton').click();

        cy.get('.pluginList').contains('Minecraft-Panel-Plugin')

        cy.get('table', { timeout: 5000 }).should('be.visible')./*eq(0).*/contains('td', 'Minecraft-Panel-Plugin')
    })
})