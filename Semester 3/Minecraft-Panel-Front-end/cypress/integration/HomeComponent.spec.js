describe('ServerComponent.vue', () => {
    it('render "login" on home component', () => {
        cy.visit("http://localhost:8080/#/")

        cy.get('.loginRedirect').contains('login');
    })
})