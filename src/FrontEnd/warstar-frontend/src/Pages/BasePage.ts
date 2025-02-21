// BasePage.ts
import React from 'react';
import { UserModel } from '../Models/UserModel';

export interface BasePage<P = {}> extends React.FC<P & { loggedInUser: UserModel | null }> {
   displayName: string;
}
