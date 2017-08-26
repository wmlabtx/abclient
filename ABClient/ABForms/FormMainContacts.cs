namespace ABClient.ABForms
{
    using System.Windows.Forms;
    using MyForms;
    using Properties;
    using Tabs;
    using System;
  
    internal sealed partial class FormMain
    {
        private void tsContactTrace_Click(object sender, EventArgs e)
        {
            AppVars.Profile.DoContactTrace = tsContactTrace.Checked;
        }

        private void tsBossTrace_Click(object sender, EventArgs e)
        {
            AppVars.Profile.DoBossTrace = tsBossTrace.Checked;
        }

        internal void AddContactFromBulk(string nick)
        {
            ContactsManager.Add(treeContacts, nick);
        }

        private void AddContact(string nick)
        {
            ContactsManager.Add(treeContacts, nick);
        }

        private void DeleteContact()
        {
            var tn = treeContacts.SelectedNode;
            if (tn.Tag == null)
            {
                return;
            }

            ContactsManager.Remove(treeContacts, tn);
        }

        private void SelectContact(TreeNode tn)
        {
            if (tn.Tag == null)
            {
                tsDeleteContact.Enabled = false;
                tsContactPrivate.Enabled = false;
                cmtsDeleteContact.Enabled = false;
                cmtsContactPrivate.Enabled = false;
                tbContactDetails.Text = string.Empty;
                miRemoveGroup.Enabled = true;
            }
            else
            {
                var contact = (Contact)tn.Tag;
                tbContactDetails.Text = contact.Comments;
                tsDeleteContact.Enabled = true;
                cmtsDeleteContact.Enabled = true;
                tsContactPrivate.Enabled = true;
                cmtsContactPrivate.Enabled = true;

                cmtsClassNeutral.Checked = false;
                cmtsClassFoe.Checked = false;
                cmtsClassFriend.Checked = false;
                switch (contact.ClassId)
                {
                    case 0:
                        cmtsClassNeutral.Checked = true;
                        break;
                    case 1:
                        cmtsClassFoe.Checked = true;
                        break;
                    case 2:
                        cmtsClassFriend.Checked = true;
                        break;
                    default:
                        cmtsClassNeutral.Checked = true;
                        break;
                }

                cmtsToolId0.Checked = false;
                cmtsToolId1.Checked = false;
                cmtsToolId2.Checked = false;
                cmtsToolId3.Checked = false;
                cmtsToolId4.Checked = false;
                cmtsToolId5.Checked = false;
                switch (contact.ToolId)
                {
                    case 0:
                        cmtsToolId0.Checked = true;
                        break;
                    case 1:
                        cmtsToolId1.Checked = true;
                        break;
                    case 2:
                        cmtsToolId2.Checked = true;
                        break;
                    case 3:
                        cmtsToolId3.Checked = true;
                        break;
                    case 4:
                        cmtsToolId4.Checked = true;
                        break;
                    case 5:
                        cmtsToolId5.Checked = true;
                        break;
                    default:
                        cmtsToolId0.Checked = true;
                        break;
                }
            }
        }

        private void CommentContact()
        {
            var tn = treeContacts.SelectedNode;
            if (tn?.Tag == null)
                return;

            var ce = (Contact)tn.Tag;
            ce.Comments = tbContactDetails.Text ?? string.Empty;
            ContactsManager.UpdateComments(ce, ce.Comments);
        }

        private void OpenContact()
        {
            var contact = GetContact();
            if ((contact == null) || string.IsNullOrEmpty(contact.Name))
            {
                return;
            }

            CreateNewTab(TabType.PInfo, Resources.AddressPInfo + contact.Name, false);
        }

        private void OpenQuickFromContact()
        {
            var contact = GetContact();
            if ((contact == null) || string.IsNullOrEmpty(contact.Name))
            {
                return;
            }

            var formQuick = new FormQuick(contact.Name);
            formQuick.Show();
        }

        private Contact GetContact()
        {
            if (treeContacts == null)
            {
                return null;
            }

            if (treeContacts.SelectedNode == null)
            {
                return null;
            }

            var tn = treeContacts.SelectedNode;

            var contact = (Contact) tn.Tag;
            return contact;
        }


        private void WriteContactPrivate()
        {
            if (treeContacts != null)
            {
                var tn = treeContacts.SelectedNode;
                if (tn.Tag == null)
                {
                    return;
                }

                var ce = (Contact)tn.Tag;
                WriteMessageToPrompt("%<" + ce.Name + "> ");
            }

            tabControlLeft.SelectedIndex = 0;
        }

        private void SetContactClass(int classid)
        {
            var tn = treeContacts.SelectedNode;

            var contact = (Contact) tn.Tag;
            if (contact == null)
                return;

            contact.ClassId = classid;
            if (!AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                return;

            AppVars.Profile.Contacts[contact.Name.ToLower()].ClassId = classid;
            if (!tn.Checked)
                return;

            tn.ForeColor = ContactsManager.GetColorOfContact(contact);
            SelectContact(tn);
        }

        private void CmtsClassNeutralClick(object sender, EventArgs e)
        {
            SetContactClass(0);
        }

        private void CmtsClassFoeClick(object sender, EventArgs e)
        {
            SetContactClass(1);
        }

        private void CmtsClassFriendClick(object sender, EventArgs e)
        {
            SetContactClass(2);
        }

        private void SetContactToolId(int toolid)
        {
            var tn = treeContacts.SelectedNode;

            var contact = (Contact) tn.Tag;
            if (contact == null)
                return;
            
            if (!AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                return;

            AppVars.Profile.Contacts[contact.Name.ToLower()].ToolId = toolid;
            SelectContact(tn);
        }

        private void CmtsToolId0Click(object sender, EventArgs e)
        {
            SetContactToolId(0);
        }

        private void CmtsToolId1Click(object sender, EventArgs e)
        {
            SetContactToolId(1);
        }

        private void CmtsToolId2Click(object sender, EventArgs e)
        {
            SetContactToolId(2);
        }

        private void CmtsToolId3Click(object sender, EventArgs e)
        {
            SetContactToolId(3);
        }

        private void CmtsToolId4Click(object sender, EventArgs e)
        {
            SetContactToolId(4);
        }

        private void CmtsToolId5Click(object sender, EventArgs e)
        {
            SetContactToolId(5);
        }

        private void SetGroupClass(int classid)
        {
            var tngroup = treeContacts.SelectedNode;
            if (tngroup.Tag != null)
                return;

            foreach (TreeNode tn in tngroup.Nodes)
            {
                var contact = (Contact)tn.Tag;
                if (contact == null)
                    continue;

                if (!AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                    continue;

                AppVars.Profile.Contacts[contact.Name.ToLower()].ClassId = classid;
                if (!tn.Checked)
                    continue;

                tn.ForeColor = ContactsManager.GetColorOfContact(contact);
            }
        }

        private void MiSetGroupNeutralClick(object sender, EventArgs e)
        {
            SetGroupClass(0);
        }

        private void MiSetGroupFoeClick(object sender, EventArgs e)
        {
            SetGroupClass(1);
        }

        private void MiSetGroupFriendClick(object sender, EventArgs e)
        {
            SetGroupClass(2);
        }

        private void SetGroupToolId(int toolid)
        {
            var tngroup = treeContacts.SelectedNode;
            if (tngroup.Tag != null)
                return;

            foreach (TreeNode tn in tngroup.Nodes)
            {
                var contact = (Contact)tn.Tag;
                if (contact == null)
                    continue;

                if (!AppVars.Profile.Contacts.ContainsKey(contact.Name.ToLower()))
                    continue;

                AppVars.Profile.Contacts[contact.Name.ToLower()].ToolId = toolid;
            }
        }

        private void MiSetGroupToolId0Click(object sender, EventArgs e)
        {
            SetGroupToolId(0);
        }

        private void MiSetGroupToolId1Click(object sender, EventArgs e)
        {
            SetGroupToolId(1);
        }

        private void MiSetGroupToolId2Click(object sender, EventArgs e)
        {
            SetGroupToolId(2);
        }

        private void MiSetGroupToolId3Click(object sender, EventArgs e)
        {
            SetGroupToolId(3);
        }

        private void MiSetGroupToolId4Click(object sender, EventArgs e)
        {
            SetGroupToolId(4);
        }

        private void MiSetGroupToolId5Click(object sender, EventArgs e)
        {
            SetGroupToolId(5);
        }
    }
}