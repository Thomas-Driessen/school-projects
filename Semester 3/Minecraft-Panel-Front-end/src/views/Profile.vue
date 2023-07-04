<template>
  <div class="container">
    <div class="profileOverview row">
      <ProfileOverviewComponent />
    </div>
    <div class="resetDetails row">
      <div class="col-lg-6 passwordResetCollapse">
        <div>
          <b-button v-b-toggle.collapse-password variant="primary">Reset password</b-button>
          <b-collapse id="collapse-password" class="mt-2">
            <PasswordResetComponent />
          </b-collapse>
        </div>
      </div>
      <div class="col-lg-6 emailResetCollapse">
        <b-button v-b-toggle.collapse-email variant="primary">Reset email</b-button>
        <b-collapse id="collapse-email" class="mt-2">
          <EmailResetComponent />
        </b-collapse>
      </div>
    </div>
  </div>
</template>

<script>
import PasswordResetComponent from "@/components/PasswordResetComponent";
import ProfileOverviewComponent from "@/components/ProfileOverviewComponent";
import EmailResetComponent from "@/components/EmailResetComponent";

export default {
  name: 'Profile',
  components: {
    EmailResetComponent,
    ProfileOverviewComponent,
    PasswordResetComponent
  },
  computed: {
    currentUser() {
      return this.$store.state.auth.user;
    }
  },
  mounted() {
    if (this.currentUser != null && !this.currentUser) {
      this.$router.push('/login');
    }
  }
};
</script>
