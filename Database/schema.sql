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

-- 8. Seed Audiobooks (Officially classified by class from @Stories_of_Inclusion playlists)
INSERT INTO public.audiobooks (id, title_en, title_bn, grade_level, youtube_url, duration_seconds, transcript_bn, transcript_en) VALUES
(1, 'Class 1 | Amar Bangla Boi | পাঠ - ১০ থেকে ১৬ (বর্ণ শিখি) | ১ম পর্ব | অ থেকে ঊ', '১ম শ্রেণি, আমার বাংলা বই, পাঠ - ১০ থেকে ১৬ (বর্ণ শিখি) | ১ম পর্ব | অ থেকে ঊ', 'Class 1', 'https://www.youtube.com/watch?v=PUCU1FURW7k', 185, 'দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।', 'Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners.'),
(2, 'Class 1 | Amar Bangla Boi | পাঠ - ১৯, ২০ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ১ম পর্ব | ক থেকে ঞ', '১ম শ্রেণি, আমার বাংলা বই, পাঠ - ১৯, ২০ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ১ম পর্ব | ক থেকে ঞ', 'Class 1', 'https://www.youtube.com/watch?v=N1LM6uDG3pw', 195, 'দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।', 'Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners.'),
(3, 'Class 1 | Amar Bangla Boi | পাঠ - ২৩, ২৪ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ২য় পর্ব | ট থেকে ন', '১ম শ্রেণি, আমার বাংলা বই, পাঠ - ২৩, ২৪ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ২য় পর্ব | ট থেকে ন', 'Class 1', 'https://www.youtube.com/watch?v=mwhRWKfLxHs', 193, 'দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।', 'Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners.'),
(4, 'Class 1 | Amar Bangla Boi | পাঠ - ২৬, ৩২ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৩য় পর্ব | প থেকে ষ', '১ম শ্রেণি, আমার বাংলা বই, পাঠ - ২৬, ৩২ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৩য় পর্ব | প থেকে ষ', 'Class 1', 'https://www.youtube.com/watch?v=i2SK4VcvnU4', 188, 'দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।', 'Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners.'),
(5, 'Class 1 | Amar Bangla Boi | পাঠ - ৩৪ ৩৯ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৪র্থ পর্ব | স থেকে চন্দ্রবিন্দু', '১ম শ্রেণি, আমার বাংলা বই, পাঠ - ৩৪ ৩৯ (বর্ণ শিখি) | ব্যঞ্জনবর্ণ | ৪র্থ পর্ব | স থেকে চন্দ্রবিন্দু', 'Class 1', 'https://www.youtube.com/watch?v=luIqWumEQ-c', 169, 'দৃষ্টিপ্রতিবন্ধী শিক্ষার্থীদের জন্য প্রথম শ্রেণির বাংলা পাঠ্যবইয়ের সহজ ও স্পষ্ট বর্ণশিক্ষা। সঠিক উচ্চারণ ও ধ্বনির সমন্বয়ে তৈরি অডিও পাঠ।', 'Clear, accessible phonics and alphabet learning audio lesson for Class 1 primary students, carefully voiced for visually challenged and early learners.'),
(6, 'Class 2 | Amar Bangla Boi | পাঠ - ৬ | আয় দেখে যা নাচ', '২য় শ্রেণি, আমার বাংলা বই, পাঠ - ৬ | আয় দেখে যা নাচ', 'Class 2', 'https://www.youtube.com/watch?v=jbAp8rDZDnk', 119, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(7, 'Class 2 | Amar Bangla Boi | পাঠ -১০ | একুশের গান', '২য় শ্রেণি, আমার বাংলা বই, পাঠ -১০ | একুশের গান', 'Class 2', 'https://www.youtube.com/watch?v=cgpBgnt0s9U', 137, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(8, 'Class 2 | Amar Bangla Boi | পাঠ -৪ | ডালিমকুমার ও কঙ্কাবতী', '২য় শ্রেণি, আমার বাংলা বই, পাঠ -৪ | ডালিমকুমার ও কঙ্কাবতী', 'Class 2', 'https://www.youtube.com/watch?v=n06hpRj_Cgc', 111, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(9, 'Class 2 | Amar Bangla Boi | পাঠ - ১৪ | কাজের আনন্দ', '২য় শ্রেণি, আমার বাংলা বই, পাঠ - ১৪ | কাজের আনন্দ', 'Class 2', 'https://www.youtube.com/watch?v=7XW9S_uLJMY', 166, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(10, 'Class 2 | Amar Bangla Boi | পাঠ - ১৭ | গ্রাম ও শহর', '২য় শ্রেণি, আমার বাংলা বই, পাঠ - ১৭ | গ্রাম ও শহর', 'Class 2', 'https://www.youtube.com/watch?v=IXhvcxYo2cQ', 136, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(11, 'Class 2 | Amar Bangla Boi | পাঠ - ১৯ | বিড়াল ছানা', '২য় শ্রেণি, আমার বাংলা বই, পাঠ - ১৯ | বিড়াল ছানা', 'Class 2', 'https://www.youtube.com/watch?v=LFK6-9QtMPw', 176, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(12, 'Class 2 | Amar Bangla Boi | পাঠ - ২৫ | মুক্তির সেনা', '২য় শ্রেণি, আমার বাংলা বই, পাঠ - ২৫ | মুক্তির সেনা', 'Class 2', 'https://www.youtube.com/watch?v=44tBw9m7hOc', 161, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(13, 'Class 2 | Amar Bangla Boi | পাঠ ২২ | আমাদের ছোট নদী', '২য় শ্রেণি, আমার বাংলা বই, পাঠ ২২ | আমাদের ছোট নদী', 'Class 2', 'https://www.youtube.com/watch?v=g_VXWbFIcEI', 178, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(14, 'Class 2 | Amar Bangla Boi | পাঠ ২১ | পয়লা বৈশাখ', '২য় শ্রেণি, আমার বাংলা বই, পাঠ ২১ | পয়লা বৈশাখ', 'Class 2', 'https://www.youtube.com/watch?v=e3K90yo7y78', 141, 'দ্বিতীয় শ্রেণির শিক্ষার্থীদের জন্য সহজ ও সাবলীল ভাষায় রচিত সাহিত্য ও নীতিশিক্ষামূলক পাঠ। শ্রবণভিত্তিক বিশেষ পদ্ধতিতে প্রস্তুতকৃত।', 'Engaging primary school literature, values, and narrative lesson adapted for Class 2 students with visual impairments and assistive learning needs.'),
(15, 'Class 3 | Amar Bangla Boi | পাঠ ৩', 'আমার বাংলা বই | ৩য় শ্রেণি । পাঠ ৩', 'Class 3', 'https://www.youtube.com/watch?v=4Ut7K_m0TYQ', 190, 'তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।', 'Accessible Class 3 textbook literature lesson exploring Bangladesh''s nature, cultural heritage, and moral insights designed for auditory comprehension.'),
(16, 'Class 3 | Amar Bangla Boi | পাঠ ১২।', 'আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ১২।', 'Class 3', 'https://www.youtube.com/watch?v=T2D6Dxm7j6Y', 105, 'তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।', 'Accessible Class 3 textbook literature lesson exploring Bangladesh''s nature, cultural heritage, and moral insights designed for auditory comprehension.'),
(17, 'Class 3 | Amar Bangla Boi | পাঠ ৭।', 'আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ৭।', 'Class 3', 'https://www.youtube.com/watch?v=dij4IAuxivE', 620, 'তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।', 'Accessible Class 3 textbook literature lesson exploring Bangladesh''s nature, cultural heritage, and moral insights designed for auditory comprehension.'),
(18, 'Class 3 | Amar Bangla Boi | পাঠ ১১।', 'আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ১১।', 'Class 3', 'https://www.youtube.com/watch?v=msw6SCOi-M0', 159, 'তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।', 'Accessible Class 3 textbook literature lesson exploring Bangladesh''s nature, cultural heritage, and moral insights designed for auditory comprehension.'),
(19, 'Class 3 | Amar Bangla Boi | পাঠ ৮।', 'আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ৮।', 'Class 3', 'https://www.youtube.com/watch?v=xU_YwTn3B9k', 419, 'তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।', 'Accessible Class 3 textbook literature lesson exploring Bangladesh''s nature, cultural heritage, and moral insights designed for auditory comprehension.'),
(20, 'Class 3 | Amar Bangla Boi | পাঠ ১।', 'আমার বাংলা বই, তৃতীয় শ্রেণি, পাঠ ১।', 'Class 3', 'https://www.youtube.com/watch?v=gKbM9uX8zoM', 63, 'তৃতীয় শ্রেণির পাঠ্যপুস্তকের সাহিত্য পাঠ। প্রাকৃতিক সৌন্দর্য, মানবিক মূল্যবোধ ও বর্ণনামূলক সাহিত্যের সহজ ব্যাখ্যা।', 'Accessible Class 3 textbook literature lesson exploring Bangladesh''s nature, cultural heritage, and moral insights designed for auditory comprehension.'),
(21, 'Class 3 | English For Today | Unit 39: The Crow (Lessons 1-9)', 'Class 3 | English For Today | Unit 39 | Story : The Crow | Lessons 1-9', 'Class 3', 'https://www.youtube.com/watch?v=pT8YG_krDf0', 117, 'তৃতীয় শ্রেণির ইংরেজি পাঠ: তৃষ্ণার্ত কাকের গল্প। বুদ্ধি ও ধৈর্যের মাধ্যমে কীভাবে সমস্যা সমাধান করা যায়, তা সুন্দর ইংরেজি উচ্চারণে বর্ণিত।', 'An engaging English fable lesson for Class 3 teaching patience, critical problem-solving, and vocabulary building through the story of the thirsty crow.'),
(22, 'Class 4 | Amar Bangla Boi | পাঠ ১৭| আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ১৭| আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=GpT7DoZZlvA', 171, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(23, 'Class 4 | Amar Bangla Boi | পাঠ ১৫ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ১৫ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=-Lyo0Ksk8z0', 313, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(24, 'Class 4 | Amar Bangla Boi | পাঠ ১৪ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ১৪ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=z9cS6_Mh5xc', 124, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(25, 'Class 4 | Amar Bangla Boi | পাঠ ১৩ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ১৩ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=YZWnFprTedY', 307, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(26, 'Class 4 | Amar Bangla Boi | পাঠ ১২ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ১২ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=utQLmM_RJn4', 214, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(27, 'Class 4 | Amar Bangla Boi | পাঠ ১১ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ১১ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=wXtI43EI1C8', 194, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(28, 'Class 4 | Amar Bangla Boi | পাঠ ১০ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ১০ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=KuJ5boIYLtM', 482, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(29, 'Class 4 | Amar Bangla Boi | পাঠ ৯ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ৯ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=GQXx7oo-mOg', 316, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(30, 'Class 4 | Amar Bangla Boi | পাঠ ৭ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ৭ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=yaZRAB-0eco', 97, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(31, 'Class 4 | Amar Bangla Boi | পাঠ ৬ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ৬ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=UERysDp0fuk', 124, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(32, 'Class 4 | Amar Bangla Boi | পাঠ ৫ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ৫ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=fkzELOS145c', 150, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(33, 'Class 4 | Amar Bangla Boi | পাঠ ২ | আমার বাংলা বই', 'চতুর্থ শ্রেনি | পাঠ ২ | আমার বাংলা বই', 'Class 4', 'https://www.youtube.com/watch?v=9zjfo-B4iNo', 141, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(34, 'Class 4 | Amar Bangla Boi | পাঠ ১ । আমার বাংলা বই |', 'চতুর্থ শ্রেণি | পাঠ ১ । আমার বাংলা বই |', 'Class 4', 'https://www.youtube.com/watch?v=WwL3M0i6nRY', 88, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(35, 'Class 4 | Amar Bangla Boi | আমার বাংলা বই | পাঠ ৫', 'আমার বাংলা বই | চতুর্থ শ্রেণি | পাঠ ৫', 'Class 4', 'https://www.youtube.com/watch?v=Nvy9KNJ0xTQ', 205, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(36, 'Class 4 | Amar Bangla Boi | বাওয়ালিদের গল্প', '৪র্থ শ্রেণি, আমার বাংলা বই | বাওয়ালিদের গল্প', 'Class 4', 'https://www.youtube.com/watch?v=cFBFGQEilI8', 101, 'চতুর্থ শ্রেণির শিক্ষার্থীদের উপযোগী বাংলা পাঠ্যসূচির সম্পূর্ণ অডিও সংস্করণ। পাঠ্যক্রমের প্রতিটি শব্দ স্পষ্ট ও সহজবোধ্যভাবে উপস্থাপিত।', 'Comprehensive audio textbook lesson for Class 4 primary curriculum, mastered specifically for inclusive classrooms and blind learners.'),
(37, 'Class 4 | English For Today | Unit 34: Story - S.M. Sultan', 'Class 4| English For Today | Unit 34', 'Class 4', 'https://www.youtube.com/watch?v=d675GtUMCOw', 195, 'চতুর্থ শ্রেণির ইংরেজি পাঠ্যপুস্তক: বাংলাদেশের বিখ্যাত চিত্রশিল্পী এস এম সুলতানের জীবন ও কর্মের অনুপ্রেরণামূলক পাঠ।', 'Class 4 English lesson on the life, artistic vision, and humanitarian spirit of celebrated master painter S.M. Sultan.')
ON CONFLICT (id) DO NOTHING;

-- 9. Seed Core Team Member Profiles (Pre-configured as Members, Admins, SuperAdmins)
INSERT INTO public.profiles (id, email, full_name, role, bio_en, is_portfolio_approved) VALUES
('00000000-0000-0000-0000-000000000001', 'nusaibaiffaat3@gmail.com', 'Iffat Nusaiba', 'superadmin', 'Executive leadership, project oversight, and inclusive curriculum direction.', true),
('00000000-0000-0000-0000-000000000002', 'mjesha12@gmail.com', 'Musarrat Jahan Esha', 'admin', 'Creative direction, visual storytelling, and accessibility design strategy.', true),
('00000000-0000-0000-0000-000000000004', 'naima.taki@storiesofinclusion.org', 'Naima Jannat Taki', 'admin', 'Program design, school outreach implementation, and stakeholder relations.', true),
('00000000-0000-0000-0000-000000000005', 'rafieu.arnob@storiesofinclusion.org', 'Md. Rafieu Hossain Arnob', 'admin', 'Operational efficiency, resource allocation, and program scalability.', true),
('00000000-0000-0000-0000-000000000006', 'afnan.orthey@storiesofinclusion.org', 'Afnan Jahan Orthey', 'admin', 'Co-founding strategic direction and policy advocacy partnerships.', true),
('00000000-0000-0000-0000-000000000007', 'masruba.tanha@storiesofinclusion.org', 'Masruba Mehejabin Tanha', 'user', 'Public outreach, community engagement, and digital awareness series.', true),
('00000000-0000-0000-0000-000000000008', 'ehsan.rakib@storiesofinclusion.org', 'Ehsan Al Rakib Chowdhury', 'user', 'School campaign execution, volunteer management, and offline logistics.', true),
('00000000-0000-0000-0000-000000000009', 'kazi.airin@storiesofinclusion.org', 'Kazi Airin Sultana', 'user', 'Accessible branding, educational illustrations, and campaign identity.', true),
('00000000-0000-0000-0000-000000000010', 'ismail.polash@storiesofinclusion.org', 'Ismail Hosen Polash', 'user', 'Audiobook studio recording, audio mastering, and video production.', true)
ON CONFLICT (id) DO NOTHING;

