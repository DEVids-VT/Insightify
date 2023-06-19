import { Injectable } from '@angular/core';
import { UserManager, UserManagerSettings, User } from 'oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private manager = new UserManager(getClientSettings());
  private user: User | null = null;

  constructor() { 
    this.manager.getUser().then(user => {
      this.user = user;
    })
  }
  login(): Promise<void> {
    return this.manager.signinRedirect();
  }

  isLoggedIn(): boolean {
    return this.user != null && !this.user.expired;
  }
}
function getClientSettings(): UserManagerSettings {
  return {
    authority: 'http://localhost:5001',
    client_id: 'js',
    redirect_uri: 'http://localhost:4200/callback',
    response_type: "code",
    scope:"openid profile",
    post_logout_redirect_uri : 'http://localhost:4200',
  }
};
