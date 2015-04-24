class Api::RecordsController < Api::BaseController


	def user_login
		@email = params[:email]
		@password = params[:password]
		@user = Student.find_by_email(params[:email])
		if @user
			if @user.valid_password?(params[:password])
				render status: 200
			else
				render status: 401
			end
		else
			render status: 401
		end
	end
		

	def get_records_by_email
		#get the email that identifies the user
		@email = params[:email]
		#logic goes here to get @records
		@records = Record.where(email: @email).order(created_at: :desc) ### code here
		respond_with @records
	end


	def save_record
		#get the parameters fom the post params
		@time = params[:time]
		@score = params[:score]
		@level = params[:level]
		@clicks = params[:clicks]
		@email = params[:email]
		@logs = params[:logs]
		#create new record
		@record = Record.new(time: @time, score: @score, level: @level, clicks: @clicks, email: @email, logs: @logs)
		#if the recordsis save , respond with OK , else rrespond with  error status(errors in parametrs)
		if @record.save 
			render status: 201
		else
			render status: 422
		end

	end

  
    def get_level
		@email = params[:email]
	  	@records = Record.where(email: @email)
	  	@record = @records.last
	end


	def save_answer
		@email = params[:email]
		@quizNumber = params[:quiz]
		@question = params[:question]
		@answer = params[:answer]
		@correct = params[:correct]
		@inGameQuiz = InGameQuiz.new(email: @email, quiz: @quizNumber, question: @question, answer: @answer, correct: @correct)
		if @inGameQuiz.save
			render status: 201
		else
			render status: 422
		end
	end


end
