-- ==========================================================================
-- STORIES OF INCLUSION: POSTGRESQL & SUPABASE PRODUCTION SCHEMA
-- ==========================================================================

-- 1. Create Enums for Roles and Submissions
CREATE TYPE user_role AS ENUM ('user', 'admin', 'superadmin');
CREATE TYPE submission_type AS ENUM ('Join Us', 'Contact Inquiry');

-- 2. User Profiles Table (Linked with Supabase auth.users)
CREATE TABLE public.profiles (
    id UUID PRIMARY KEY REFERENCES auth.users(id) ON DELETE CASCADE,
    email TEXT NOT NULL,
    full_name TEXT NOT NULL,
    role user_role NOT NULL DEFAULT 'user',
    bio_en TEXT,
    bio_bn TEXT,
    avatar_url TEXT,
    is_portfolio_approved BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    updated_at TIMESTAMPTZ DEFAULT NOW()
);

-- 3. Form Submissions Table
CREATE TABLE public.form_submissions (
    id SERIAL PRIMARY KEY,
    submission_type submission_type NOT NULL DEFAULT 'Join Us',
    full_name TEXT NOT NULL,
    email TEXT NOT NULL,
    phone TEXT,
    message TEXT NOT NULL,
    status TEXT DEFAULT 'New',
    created_at TIMESTAMPTZ DEFAULT NOW()
);

-- 4. Inclusive Audiobooks Table
CREATE TABLE public.audiobooks (
    id SERIAL PRIMARY KEY,
    title_en TEXT NOT NULL,
    title_bn TEXT NOT NULL,
    grade_level TEXT NOT NULL,
    youtube_url TEXT NOT NULL,
    audio_url TEXT,
    cover_image_url TEXT,
    transcript_en TEXT,
    transcript_bn TEXT,
    duration_seconds INT DEFAULT 0,
    created_at TIMESTAMPTZ DEFAULT NOW()
);

-- 5. Enable Row Level Security (RLS)
ALTER TABLE public.profiles ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.form_submissions ENABLE ROW LEVEL SECURITY;
ALTER TABLE public.audiobooks ENABLE ROW LEVEL SECURITY;

-- 6. RLS Policies: Audiobooks are public for reading
CREATE POLICY "Public Audiobooks Viewable" ON public.audiobooks FOR SELECT USING (true);

-- 7. RLS Policies: Form Submissions
CREATE POLICY "Public can insert submissions" ON public.form_submissions FOR INSERT WITH CHECK (true);
CREATE POLICY "Admins can view submissions" ON public.form_submissions FOR SELECT USING (
    EXISTS (SELECT 1 FROM public.profiles WHERE id = auth.uid() AND role IN ('admin', 'superadmin'))
);

-- 8. Audiobooks (Populated dynamically via Admin Dashboard or API without seed placeholders)

-- 9. Seed Core Team Member Profiles (Pre-configured as Members, Admins, SuperAdmins)
INSERT INTO public.profiles (id, email, full_name, role, bio_en, is_portfolio_approved) VALUES
('00000000-0000-0000-0000-000000000001', 'nusaibaiffaat3@gmail.com', 'Iffat Nusaiba', 'superadmin', 'Executive leadership, project oversight, and inclusive curriculum direction.', true),
('00000000-0000-0000-0000-000000000002', 'mjesha12@gmail.com', 'Musarrat Jahan Esha', 'admin', 'Creative direction, visual storytelling, and accessibility design strategy.', true),
('00000000-0000-0000-0000-000000000003', 'shoaibahmed.5063@gmail.com', 'Shoaib Ahmed', 'admin', 'Lead Full-Stack Developer & Accessibility Architect.', true),
('00000000-0000-0000-0000-000000000004', 'naima.taki@storiesofinclusion.org', 'Naima Jannat Taki', 'admin', 'Program design, school outreach implementation, and stakeholder relations.', true),
('00000000-0000-0000-0000-000000000005', 'rafieu.arnob@storiesofinclusion.org', 'Md. Rafieu Hossain Arnob', 'admin', 'Operational efficiency, resource allocation, and program scalability.', true),
('00000000-0000-0000-0000-000000000006', 'afnan.orthey@storiesofinclusion.org', 'Afnan Jahan Orthey', 'admin', 'Co-founding strategic direction and policy advocacy partnerships.', true),
('00000000-0000-0000-0000-000000000007', 'masruba.tanha@storiesofinclusion.org', 'Masruba Mehejabin Tanha', 'user', 'Public outreach, community engagement, and digital awareness series.', true),
('00000000-0000-0000-0000-000000000008', 'ehsan.rakib@storiesofinclusion.org', 'Ehsan Al Rakib Chowdhury', 'user', 'School campaign execution, volunteer management, and offline logistics.', true),
('00000000-0000-0000-0000-000000000009', 'kazi.airin@storiesofinclusion.org', 'Kazi Airin Sultana', 'user', 'Accessible branding, educational illustrations, and campaign identity.', true),
('00000000-0000-0000-0000-000000000010', 'ismail.polash@storiesofinclusion.org', 'Ismail Hosen Polash', 'user', 'Audiobook studio recording, audio mastering, and video production.', true)
ON CONFLICT (id) DO NOTHING;

