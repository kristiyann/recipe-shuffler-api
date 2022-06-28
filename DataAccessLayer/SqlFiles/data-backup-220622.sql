USE [RecipeShufflerV1]
GO
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Active]) VALUES (N'0aee435b-b72b-4c55-95c6-261c70b8c508', N'test update', N'test@test.test', N'$2a$11$/XYYvx.FQHYRpeTt2ARq.e.Ho7KQW/WhozFhfG/bsAYTPRe3awWtu', 1)
GO
INSERT [dbo].[Users] ([Id], [Username], [Email], [Password], [Active]) VALUES (N'094f2d56-b063-4eeb-a823-621b00fabd65', N'kristiyan', N'kristiyan0312@abv.bg', N'$2a$11$8YsFQzqdZ2i2k9YbhQXxlewzePYupJn0w4zzXaTGxJ5YKlUu/mf7y', 1)
GO
INSERT [dbo].[Recipes] ([Id], [Title], [Ingredients], [Instructions], [UserId], [Image]) VALUES (N'0732722d-c408-4201-eb62-08da012fb656', N'Картофена супа', N'4-5 картофа, 1 глава лук, 1 чушка, 1 морков, домати (сок), сол, черен пипер, чубрица, магданоз', N'Картофите и зеленчуците се нарязват. Сипва се вода в тенджера - да заври. След това се пускат зеленчуците и врят около 30 мин. Слага се фиде и 1 лъжица домати. Супата се оставя на котлона, докато е готово фидето. Накрая се слагат подправките.', N'094f2d56-b063-4eeb-a823-621b00fabd65', N'https://edin.bg/files/lib/400x296/selska-supa-chubrica1.jpg')
GO
INSERT [dbo].[Recipes] ([Id], [Title], [Ingredients], [Instructions], [UserId], [Image]) VALUES (N'35a127fd-6adf-400f-fdd0-08da0292e08d', N'Кюфтета в доматен сос', N'Кюфтета, домати (сок), няколко скилидки чесън, брашно, червен пипер, захар, сол, магданоз, олио', N'Сипват се домати и няколко скилидки чесън в тенджера + малко олио за 3-4 мин. След това се слага и малко червен пипер като след това се разбърква. Налива се вода в тенджерата, за да заври. Слагат се 2-3 супени лъжици брашно, за да се сгъсти соса, като то се размива с вода преди това. Сипва се постепенно с едната ръка, като с другата се бърка тенджерата. 1 малка лъжица захар, за да не киселеят доматите. Слагаме кюфтетата в соса и подправяме на вкус.', N'094f2d56-b063-4eeb-a823-621b00fabd65', N'https://recepti.ezine.bg/files/lib/400x296/domateni-kufteta-stastlivi.jpg')
GO
INSERT [dbo].[Recipes] ([Id], [Title], [Ingredients], [Instructions], [UserId], [Image]) VALUES (N'4e79b1e7-ad57-4b8c-9a41-08da14e4cd53', N'Гювечета', N'1 картоф за всеки 2 гювечета, колбас, домати (сок), сирене, яйца, олио, чубрица', N'На дъното на гювечето се сипва малко олио. Поставяме картофи, нарязани на шайби. Наливат се малко домати. Нарязва се някакъв колбас по избор отгоре. Подправя се с чубрица и натрошено сирене. Пъхат се във фурната на 200-220 градуса, КАТО ТЯ НЕ ТРЯБВА ДА БЪДЕ ЗАГРЯТА ПРЕДВАРИТЕЛНО! След 30 мин се вадят и се чуква 1 яйце на всяко, след това се връщат да още 6-7 мин.', N'094f2d56-b063-4eeb-a823-621b00fabd65', N'https://moqtarecepta.com/uploads/recepta-giuveche-s-kolbas.jpg')
GO
INSERT [dbo].[Recipes] ([Id], [Title], [Ingredients], [Instructions], [UserId], [Image]) VALUES (N'd01cc3f6-ef32-44ff-93a0-08da1d64445e', N'Яхния с наденица', N'Варена наденица, 2 големи глави лук, олио, брашно, червен пипер, домати (сок), сол, чубрица, магданоз', N'Наденицата се измива и се нарязва на парчета. Нарязват се 2 глави лук, по на едро. Слагаме наденицата в тенджера с олио, за да се запържи малко. След това сипваме лука. Щом той поумекне, сипваме 2-3 лъжици брашно и 1 червен пипер + вода, за да се получи сос. Оставяме ястието да ври. Слагат се доматите - може да се размият с брашно ако искаме да е по-гъста яхнията. Подправяме със сол, чубрица и магзаноз.', N'094f2d56-b063-4eeb-a823-621b00fabd65', N'https://recepti.gotvach.bg/files/lib/600x350/selska-yahnia-nadenica.jpg')
GO
INSERT [dbo].[Recipes] ([Id], [Title], [Ingredients], [Instructions], [UserId], [Image]) VALUES (N'06bc7e33-3e86-46a1-93a1-08da1d64445e', N'Ориз на фурна', N'Ориз, зеленчуци (грах, царевица, броколи), глава лук, сол, куркума, къри, джинджифил, магданоз, черен пипер.', N'В тавата се налива олио и се изсипват зеленчуците и нарязаната на дребно глава лук. Позапържваме ги на котлона за около 5 мин. Слагаме ориз (Малко повече от кафяна чаша за 1 човек) и позапържваме и него за няколко минути. След това се налива вода (3.5 чаши : 1 чаши ориз) и се слагат подправките. Пече се във фурната, докато омекне.', N'094f2d56-b063-4eeb-a823-621b00fabd65', N'https://recepti.gotvach.bg/files/lib/400x296/jalt-oriz-furna.JPG')
GO
INSERT [dbo].[Recipes] ([Id], [Title], [Ingredients], [Instructions], [UserId], [Image]) VALUES (N'3556bdc4-022b-4e4c-3500-08da3113e202', N'Боб', N'Боб, 1 глава лук, 1 чушка, 1 морков, домати (сок), сол, джоджен, сминдух', N'Бобът се накисва предната вечер като водата го покрива доста. На следващия ден се премива, за да се изчисти. Налива се вода в тенджера с боба, да заври. След това се излива водата и се сипва нова студена. Вари се. Сипва се лук, морков, чушка и 2 лъжици домати. След като се сготви боба се слагат подправките.', N'094f2d56-b063-4eeb-a823-621b00fabd65', N'https://www.196flavors.com/wp-content/uploads/2016/09/bob-chorba-3-FP.jpg')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'e518f62f-9a4b-4834-346b-08da0293f588', N'Vegetarian', N'#4ea360', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'61d509ee-787d-4bd4-26d5-08da4260fc23', N'Test', N'#0074ff', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'ea6b6c39-a510-49ca-23bf-08da4bb6adde', N'', N'', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'5e15c4ae-2d15-4c26-23c0-08da4bb6adde', N'test', N'', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'3f158c58-51f7-45f7-5887-08da4bb6e1b6', N'test new', N'#000001', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'45d16f5f-85c4-447f-8a0f-912f1efc95b6', N'Has Pork', N'#e4e4a1', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'65b97329-9103-45eb-bf61-a6b518624851', N'Salads', N'#00CC00', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[Tags] ([Id], [Name], [Color], [UserId]) VALUES (N'3e7e3a4c-15a2-4b75-be7c-ef94e0cd740f', N'Has Poultry', N'#f5f5dc', N'094f2d56-b063-4eeb-a823-621b00fabd65')
GO
INSERT [dbo].[RecipeTag] ([RecipesId], [TagsId]) VALUES (N'0732722d-c408-4201-eb62-08da012fb656', N'e518f62f-9a4b-4834-346b-08da0293f588')
GO
INSERT [dbo].[RecipeTag] ([RecipesId], [TagsId]) VALUES (N'06bc7e33-3e86-46a1-93a1-08da1d64445e', N'e518f62f-9a4b-4834-346b-08da0293f588')
GO
INSERT [dbo].[RecipeTag] ([RecipesId], [TagsId]) VALUES (N'3556bdc4-022b-4e4c-3500-08da3113e202', N'e518f62f-9a4b-4834-346b-08da0293f588')
GO
INSERT [dbo].[RecipeTag] ([RecipesId], [TagsId]) VALUES (N'0732722d-c408-4201-eb62-08da012fb656', N'61d509ee-787d-4bd4-26d5-08da4260fc23')
GO
INSERT [dbo].[RecipeTag] ([RecipesId], [TagsId]) VALUES (N'd01cc3f6-ef32-44ff-93a0-08da1d64445e', N'45d16f5f-85c4-447f-8a0f-912f1efc95b6')
GO
