// Role-Based Access Control (RBAC) Engine
class RbacManager {
  constructor() {
    this.roles = ['guest', 'user', 'admin', 'superadmin'];
    this.currentRole = localStorage.getItem('soi_active_role') || 'guest';
    
    // Seed initial mock state if empty
    this.initDatabase();
    this.initUI();
  }

  initDatabase() {
    if (!localStorage.getItem('soi_submissions')) {
      const initialSubmissions = [
        { id: 1, type: "Join Us", name: "Tanvir Ahmed", email: "tanvir@example.com", phone: "+8801711223344", message: "Interested in volunteering for audiobook voice recording in Bangla.", date: "2026-10-25" },
        { id: 2, type: "School Partner", name: "Rahima Khatun (Headmistress)", email: "rahima@school.edu.bd", phone: "+8801819988776", message: "We want to implement the inclusive audiobook program in our primary school in Gazipur.", date: "2026-10-26" }
      ];
      localStorage.setItem('soi_submissions', JSON.stringify(initialSubmissions));
    }

    if (!localStorage.getItem('soi_member_profile')) {
      const initialProfile = {
        name: "Iffat Nusaiba",
        role: "user",
        bio: "Co-Founder passionate about accessible education and assistive technology.",
        status: "approved",
        projects: ["Primary Audiobook Class 1", "Sensory Awareness Session"]
      };
      localStorage.setItem('soi_member_profile', JSON.stringify(initialProfile));
    }

    const teamMembersList = [
      { id: 'usr_1', name: "Iffat Nusaiba", email: "nusaibaiffaat3@gmail.com", role: "superadmin" },
      { id: 'usr_2', name: "Musarrat Jahan Esha", email: "mjesha12@gmail.com", role: "admin" },
      { id: 'usr_3', name: "Shoaib Ahmed", email: "shoaibahmed.5063@gmail.com", role: "admin" },
      { id: 'usr_4', name: "Naima Jannat Taki", email: "naima.taki@storiesofinclusion.org", role: "admin" },
      { id: 'usr_5', name: "Md. Rafieu Hossain Arnob", email: "rafieu.arnob@storiesofinclusion.org", role: "admin" },
      { id: 'usr_6', name: "Afnan Jahan Orthey", email: "afnan.orthey@storiesofinclusion.org", role: "admin" },
      { id: 'usr_7', name: "Masruba Mehejabin Tanha", email: "masruba.tanha@storiesofinclusion.org", role: "user" },
      { id: 'usr_8', name: "Ehsan Al Rakib Chowdhury", email: "ehsan.rakib@storiesofinclusion.org", role: "user" },
      { id: 'usr_9', name: "Kazi Airin Sultana", email: "kazi.airin@storiesofinclusion.org", role: "user" },
      { id: 'usr_10', name: "Ismail Hosen Polash", email: "ismail.polash@storiesofinclusion.org", role: "user" },
      { id: 'usr_11', name: "Tanvir Hasan", email: "tanvir.hasan@member.org", role: "user" }
    ];
    localStorage.setItem('soi_user_list', JSON.stringify(teamMembersList));
  }

  initUI() {
    this.updateRoleBadges();
    this.setupModalListeners();
  }

  updateRoleBadges() {
    const badge = document.getElementById('current-role-badge');
    if (badge) {
      badge.textContent = this.formatRoleName(this.currentRole);
      badge.className = `role-pill role-${this.currentRole}`;
    }

    // Role-specific sections visibility
    document.querySelectorAll('[data-role-required]').forEach(el => {
      const requiredRole = el.getAttribute('data-role-required');
      if (this.hasPermission(requiredRole)) {
        el.style.display = '';
      } else {
        el.style.display = 'none';
      }
    });
  }

  hasPermission(requiredRole) {
    const hierarchy = { guest: 0, user: 1, admin: 2, superadmin: 3 };
    return hierarchy[this.currentRole] >= hierarchy[requiredRole];
  }

  formatRoleName(role) {
    switch (role) {
      case 'superadmin': return 'SuperAdmin';
      case 'admin': return 'Staff Admin';
      case 'user': return 'Member (User)';
      default: return 'Public Visitor';
    }
  }

  setRole(role) {
    if (this.roles.includes(role)) {
      this.currentRole = role;
      localStorage.setItem('soi_active_role', role);
      this.updateRoleBadges();
      this.renderDashboard();
      
      const a11y = window.a11yManager;
      if (a11y) a11y.announce(`Active role switched to ${this.formatRoleName(role)}`);
    }
  }

  setupModalListeners() {
    const openBtn = document.getElementById('btn-open-rbac-modal');
    const modal = document.getElementById('rbac-modal');
    const closeBtn = document.getElementById('btn-close-rbac-modal');

    openBtn?.addEventListener('click', () => {
      this.renderDashboard();
      modal?.classList.add('active');
      modal?.querySelector('.modal-dialog')?.focus();
    });

    closeBtn?.addEventListener('click', () => {
      modal?.classList.remove('active');
      openBtn?.focus();
    });

    modal?.addEventListener('click', (e) => {
      if (e.target === modal) {
        modal.classList.remove('active');
        openBtn?.focus();
      }
    });

    // Handle Escape key
    document.addEventListener('keydown', (e) => {
      if (e.key === 'Escape' && modal?.classList.contains('active')) {
        modal.classList.remove('active');
        openBtn?.focus();
      }
    });
  }

  renderDashboard() {
    const container = document.getElementById('rbac-content-area');
    if (!container) return;

    // 1. Role Selection Grid
    let html = `
      <div class="role-cards-grid">
        <div class="role-card ${this.currentRole === 'guest' ? 'selected' : ''}" onclick="window.rbacManager.setRole('guest')">
          <h4><span aria-hidden="true">🌐</span> Public Visitor</h4>
          <p>Access landing page, audiobooks, accessibility tools, and submit inquiries.</p>
        </div>
        <div class="role-card ${this.currentRole === 'user' ? 'selected' : ''}" onclick="window.rbacManager.setRole('user')">
          <h4><span aria-hidden="true">👤</span> Member (User)</h4>
          <p>Manage individual member portfolio, edit bio, and view approval status.</p>
        </div>
        <div class="role-card ${this.currentRole === 'admin' ? 'selected' : ''}" onclick="window.rbacManager.setRole('admin')">
          <h4><span aria-hidden="true">🛡️</span> Staff Admin</h4>
          <p>Moderate submissions, approve portfolios, and manage content.</p>
        </div>
        <div class="role-card ${this.currentRole === 'superadmin' ? 'selected' : ''}" onclick="window.rbacManager.setRole('superadmin')">
          <h4><span aria-hidden="true">⚡</span> SuperAdmin</h4>
          <p>Full system control, manage admin permissions, view audit logs.</p>
        </div>
      </div>
    `;

    // 2. Role-Specific Interactive Panel
    if (this.currentRole === 'user') {
      const profile = JSON.parse(localStorage.getItem('soi_member_profile') || '{}');
      html += `
        <div class="dashboard-panel">
          <h4 style="margin-bottom:12px; font-weight:800;">Member Portfolio Console</h4>
          <p style="margin-bottom:12px;"><strong>Portfolio Status:</strong> <span style="color:var(--color-success); font-weight:700;">${profile.status?.toUpperCase()}</span></p>
          <div class="form-group">
            <label class="form-label">Full Name:</label>
            <input type="text" id="user-profile-name" class="form-input" value="${profile.name || ''}" />
          </div>
          <div class="form-group">
            <label class="form-label">Member Bio (for public portfolio):</label>
            <textarea id="user-profile-bio" class="form-textarea" rows="3">${profile.bio || ''}</textarea>
          </div>
          <button type="button" class="btn btn-primary" onclick="window.rbacManager.saveUserProfile()">Save Profile Changes</button>
        </div>
      `;
    } else if (this.currentRole === 'admin' || this.currentRole === 'superadmin') {
      const submissions = JSON.parse(localStorage.getItem('soi_submissions') || '[]');
      html += `
        <div class="dashboard-panel">
          <h4 style="margin-bottom:12px; font-weight:800;">Staff Admin Console: Recent Submissions</h4>
          ${submissions.length === 0 ? '<p>No submissions found.</p>' : `
            <table style="width:100%; border-collapse:collapse; font-size:0.875rem;">
              <thead>
                <tr style="text-align:left; border-bottom:2px solid var(--border-color);">
                  <th style="padding:8px;">Date</th>
                  <th style="padding:8px;">Type</th>
                  <th style="padding:8px;">Applicant</th>
                  <th style="padding:8px;">Message</th>
                  <th style="padding:8px;">Action</th>
                </tr>
              </thead>
              <tbody>
                ${submissions.map((s, idx) => `
                  <tr style="border-bottom:1px solid var(--border-color);">
                    <td style="padding:8px;">${s.date}</td>
                    <td style="padding:8px;"><span class="badge" style="background:#E0F2FE; color:#0369A1; padding:2px 8px; border-radius:4px; font-weight:700;">${s.type}</span></td>
                    <td style="padding:8px;"><strong>${s.name}</strong><br><small>${s.email}</small></td>
                    <td style="padding:8px;">${s.message}</td>
                    <td style="padding:8px;">
                      <button type="button" style="background:var(--color-danger); color:#fff; border:none; padding:4px 8px; border-radius:4px; cursor:pointer;" onclick="window.rbacManager.deleteSubmission(${idx})">Delete</button>
                    </td>
                  </tr>
                `).join('')}
              </tbody>
            </table>
          `}
        </div>
      `;

      if (this.currentRole === 'superadmin') {
        const users = JSON.parse(localStorage.getItem('soi_user_list') || '[]');
        html += `
          <div class="dashboard-panel" style="margin-top:20px;">
            <h4 style="margin-bottom:12px; font-weight:800;">SuperAdmin System Console: User Role Management</h4>
            <table style="width:100%; border-collapse:collapse; font-size:0.875rem;">
              <thead>
                <tr style="text-align:left; border-bottom:2px solid var(--border-color);">
                  <th style="padding:8px;">Name</th>
                  <th style="padding:8px;">Email</th>
                  <th style="padding:8px;">Assigned Role</th>
                  <th style="padding:8px;">Permissions</th>
                </tr>
              </thead>
              <tbody>
                ${users.map((u, i) => `
                  <tr style="border-bottom:1px solid var(--border-color);">
                    <td style="padding:8px;"><strong>${u.name}</strong></td>
                    <td style="padding:8px;">${u.email}</td>
                    <td style="padding:8px;"><span class="role-pill" style="font-size:0.75rem;">${u.role}</span></td>
                    <td style="padding:8px;">
                      <select onchange="window.rbacManager.changeUserRole(${i}, this.value)" style="padding:4px 8px; border-radius:4px; border:1px solid var(--border-color);">
                        <option value="user" ${u.role === 'user' ? 'selected' : ''}>User</option>
                        <option value="admin" ${u.role === 'admin' ? 'selected' : ''}>Admin</option>
                        <option value="superadmin" ${u.role === 'superadmin' ? 'selected' : ''}>SuperAdmin</option>
                      </select>
                    </td>
                  </tr>
                `).join('')}
              </tbody>
            </table>
          </div>
        `;
      }
    }

    container.innerHTML = html;
  }

  saveUserProfile() {
    const name = document.getElementById('user-profile-name')?.value;
    const bio = document.getElementById('user-profile-bio')?.value;
    const profile = JSON.parse(localStorage.getItem('soi_member_profile') || '{}');
    profile.name = name;
    profile.bio = bio;
    localStorage.setItem('soi_member_profile', JSON.stringify(profile));
    alert("Profile changes saved successfully!");
  }

  deleteSubmission(index) {
    const submissions = JSON.parse(localStorage.getItem('soi_submissions') || '[]');
    submissions.splice(index, 1);
    localStorage.setItem('soi_submissions', JSON.stringify(submissions));
    this.renderDashboard();
  }

  changeUserRole(userIndex, newRole) {
    const users = JSON.parse(localStorage.getItem('soi_user_list') || '[]');
    if (users[userIndex]) {
      users[userIndex].role = newRole;
      localStorage.setItem('soi_user_list', JSON.stringify(users));
      this.renderDashboard();
    }
  }
}

window.rbacManager = new RbacManager();

