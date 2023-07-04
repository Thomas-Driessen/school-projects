describe('LoginComponent.vue', () => {
    it('checks if you are redirected when not logged in', () => {
        cy.visit("http://localhost:8080/#/profile")

        cy.get('.swal2-title').contains('Unauthorized!')

        cy.url().should('include', '/login')
    });

    it('Display toast error when there is a login failure ', () => {
        cy.visit("http://localhost:8080/#/")

        cy.get('.toggleSideBarMenu').click();

        cy.contains('Login').click();

        cy.get('#input-1').type(Cypress.env('testUserEmail'));
        cy.get('#input-2').type('WrongPassword');

        cy.get('.loginForm').then(form$ => {
            form$.on("submit", e => {
                e.preventDefault();
            });
        });

        cy.get('.loginForm').submit();
        cy.wait(3000);
        cy.get('.toast-header').contains('Error');
    });
})