class TeachersController < ApplicationController
  def show
  

  end

  def send_message
   @message=Mess.new
   #this was created to pass student id of 
   #to the next page in the params
   @var=params[:student_id]
   end
    

   def submit 
      @message=Mess.new
      @message.semail=current_teacher.email
      @message.remail=Student.find(params[:student_id]).email
      @message.text=params[:my_input]
      @message.save
      redirect_to view_school_verified_students_teachers_path
  end 


   def view_courses
	   @sub = Subject.where(school: current_teacher.school)
	   @taken_id=TeacherRequestSubject.where(teacher: current_teacher , verified: true ).pluck(:subject_id)
	   @subjects=@sub.where.not(id:@taken_id) #i have all records with false not accepted and nill pending
	   @not_accepted=TeacherRequestSubject.where(teacher: current_teacher , verified: false ).pluck(:subject_id)
	   @subjects=@subjects.where.not(id:@not_accepted) 
	   @not_yet=TeacherRequestSubject.where(teacher: current_teacher , verified: nil ).pluck(:subject_id)
	   @subjects2=@subjects.where(id:@not_yet) #3shan azhr gambha pending 
	   @subjects=@subjects.where.not(id:@not_yet) 
   end


   def  request_subject
     @request=TeacherRequestSubject.new
     @request.teacher_id=current_teacher.id
     @request.subject_id=params[:subject_id]
     @request.school=current_teacher.school
     #@request.school_admin_id=current_teacher.school_admin_id
     @request.save
     redirect_to view_courses_teachers_path  
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


	def view_school_verified_students
		@current_teacher = current_teacher
		# get the sorting method if available
		@method = params[:filter_method]
		@search = params[:search]
		if @current_teacher && @current_teacher.verified
			@students = Student.where("school = ? AND verified = ?" , 
				@current_teacher.school, true)
			# sort only if there sre students and if the school admin wants to sort
			if @method && @students && @search
				if @method == "1" #sort by grade
					@students = @students.where(grade: @search)
				elsif @method == "2" #sort by name
					@students = @students.where(student_name: @search)
				elsif @method == "3" #sort by signing up date
					@students = @students.where(created_at: @search)
				elsif @method == "4"
					@students = @students.where(student_class: @search)
				end
			end
		else
			@students = {}
		end
	end


	def view_students_grades_in_questions
		@student_id = params[:student_id]
		@teacher = current_teacher
		if @teacher.verified
			if @student_id
				@students = Student.where("id = ?", @student_id)
			else
				@students = Student.where("school = ? AND verified = ?", @teacher.school, true)
			end
		end
	end


	def view_contacts
		if current_teacher.verified
			@user=current_teacher
			@teacher=Teacher.where(school:current_teacher.school)
			@t=Teacher.where(school: @user.school, verified: true)
	       @teacher=@t.where.not(id:@user.id)
	   else 
	   	@teacher = {}
	   end
	end


	def view_messages
	  @user=current_teacher
	  @sender=Teacher.find(params[:teacher_id]) 
	  @message=Mess.where(semail:@sender.email)
	end


	def view_messages_students
	  @user=current_teacher
	  @sender=Student.find(params[:student_id]) 
	  @message=Mess.where("((semail = ? AND remail = ?) OR(semail = ? AND remail = ?))" ,
	  	@sender.email, current_teacher.email, current_teacher.email, 
	  	@sender.email).order(created_at: :asc)
	  #form
	  @new_message=Mess.new
	   @var=params[:student_id]
	end


	def view_in_game_grades
		if current_teacher.verified
			@students = Student.where(school: current_teacher.school)
		else
			@students = {}
		end
	end


	def view_school_subjects
		if current_teacher.verified
			@subjects = Subject.where(school: current_teacher.school)
		else
			@subjects = {}
		end
	end
end