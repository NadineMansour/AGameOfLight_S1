class TeachersController < ApplicationController
	def show
	end


	def view_game_records
		@current_teacher = current_teacher
		if @current_teacher && @current_teacher.verified
			# get students of their school who are verified to display their scores in the view
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
		else
			@students = {}
		end
	end

	def send_message
      @message=Message.new
      session[:x]=params[:student_id]
    end 

	
	def submit 
      @message=Message.new
      @message.semail=Teacher.find(1)
      @message.remail=session[:x]
      @message.text=params[:my_input]
       session = {}
       @message.save
       redirect_to view_school_verified_students_teachers_path
      

     
	end 



	def view_school_verified_students
		@current_teacher = Teacher.find(1)
		# get the sorting method if available
		@method = params[:sort_method]
		@order = params[:order_method]
		if @current_teacher && @current_teacher.verified
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
			# sort only if there sre students and if the school admin wants to sort
			if @method && @students && @order
				if @method == "1" #sort by grade
					if @order == "1"
						@students = @students.order(grade: :asc)
					else
						@students = @students.order(grade: :desc)
					end
				elsif @method == "2" #sort by name
					if @order == "1"
						@students = @students.order(student_name: :asc)
					else
						@students = @students.order(student_name: :desc)
					end
				elsif @method == "3" #sort by signing up date
					if @order == "1"
						@students = @students.order(created_at: :asc)
					else
						@students = @students.order(created_at: :desc)
					end
				end
			end
		else
			@students = {}
		end
	end
end